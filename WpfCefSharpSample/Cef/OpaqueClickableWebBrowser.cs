using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CefSharp.Wpf;

namespace WpfCefSharpSample.Cef
{
    public class OpaqueClickableWebBrowser : ChromiumWebBrowser
    {
        private const byte HitTestAlphaThresholdDefaultValue = 0;

        #region Bindable properties

        public static readonly DependencyProperty EventTargetControlProperty = DependencyProperty.Register(
            nameof(EventTargetControl),
            typeof(Control),
            typeof(OpaqueClickableWebBrowser),
            new FrameworkPropertyMetadata(null)
        );

        public Control EventTargetControl
        {
            get { return (Control)GetValue(EventTargetControlProperty); }
            set { SetValue(EventTargetControlProperty, value); }
        }

        public static readonly DependencyProperty HitTestAlphaThresholdProperty = DependencyProperty.Register(
            nameof(HitTestAlphaThreshold),
            typeof(byte),
            typeof(OpaqueClickableWebBrowser),
            new FrameworkPropertyMetadata(HitTestAlphaThresholdDefaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public byte HitTestAlphaThreshold
        {
            get { return (byte)GetValue(HitTestAlphaThresholdProperty); }
            set { SetValue(HitTestAlphaThresholdProperty, value); }
        }

        #endregion

        #region Mouse Events

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseEnter);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseMove);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseLeave);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseWheel);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseDown);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseUp);
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseDoubleClick);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseLeftButtonDown);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseLeftButtonUp);
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseRightButtonDown);
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            HandleOrDelegateMouseEvent(e, base.OnMouseRightButtonUp);
        }

        private void HandleOrDelegateMouseEvent<T>(T eventArgs, Action<T> baseHandler)
            where T : MouseEventArgs
        {
            HandleOrDelegateEvent(e => IsHitByEvent(eventArgs.GetPosition(this)), eventArgs, baseHandler);
        }

        #endregion

        #region Context Menu Events

        protected override void OnContextMenuOpening(ContextMenuEventArgs eventArgs)
        {
            var hitPoint = new Point(eventArgs.CursorLeft, eventArgs.CursorTop);
            HandleOrDelegateEvent(e => IsHitByEvent(hitPoint), eventArgs, base.OnContextMenuOpening);
        }

        protected override void OnContextMenuClosing(ContextMenuEventArgs eventArgs)
        {
            var hitPoint = new Point(eventArgs.CursorLeft, eventArgs.CursorTop);
            HandleOrDelegateEvent(e => IsHitByEvent(hitPoint), eventArgs, base.OnContextMenuOpening);
        }

        #endregion

        #region Touch Events

        protected override void OnTouchEnter(TouchEventArgs e)
        {
            HandleOrDelegateTouchEvent(e, base.OnTouchEnter);
        }

        protected override void OnTouchMove(TouchEventArgs e)
        {
            HandleOrDelegateTouchEvent(e, base.OnTouchMove);
        }

        protected override void OnTouchLeave(TouchEventArgs e)
        {
            HandleOrDelegateTouchEvent(e, base.OnTouchLeave);
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            HandleOrDelegateTouchEvent(e, base.OnTouchDown);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            HandleOrDelegateTouchEvent(e, base.OnTouchUp);
        }

        private void HandleOrDelegateTouchEvent<T>(T eventArgs, Action<T> baseFunction)
            where T : TouchEventArgs
        {
            HandleOrDelegateEvent(e => IsHitByEvent(e.GetTouchPoint(this).Position), eventArgs, baseFunction);
        }

        #endregion

        private void HandleOrDelegateEvent<T>(Predicate<T> handleSelf, T eventArgs, Action<T> baseHandler)
            where T : RoutedEventArgs
        {
            var needsSelfHandling = handleSelf(eventArgs);
            var forceSelfHandling = (eventArgs is MouseButtonEventArgs) || (eventArgs is ContextMenuEventArgs);

            if (needsSelfHandling || forceSelfHandling)
                baseHandler(eventArgs);

            if (!needsSelfHandling)
                EventTargetControl?.RaiseEvent(eventArgs);
        }

        /// <summary>
        /// Tests this control for a hit. Intended for usage in point events.
        /// </summary>
        /// <param name="hitPoint">The point where the hit should be checked for</param>
        /// <returns>Whether this control was hit or not.</returns>
        private bool IsHitByEvent(Point hitPoint)
        {
            // Perform the hit test against a given portion of the visual object tree.
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(this, hitPoint);
            if (hitTestResult == null)
                return false;

            return !IsTransparentAtHitTestResultPoint(hitPoint, hitTestResult);
        }

        private bool IsTransparentAtHitTestResultPoint(Point hitPoint, HitTestResult hitTestResult)
        {
            try
            {
                // determine alpha value of the pixel at the click point
                var bmp = (BitmapSource)((Image)hitTestResult.VisualHit).Source;

                hitPoint = TranslateHitpointToBitmap(bmp, hitPoint);

                byte[] pixels = new byte[4];
                var rectToCheck = new Int32Rect((int)hitPoint.X, (int)hitPoint.Y, 1, 1);
                var minimumStride = (rectToCheck.Width * bmp.Format.BitsPerPixel + 7) / 8;

                bmp.CopyPixels(rectToCheck, pixels, minimumStride, 0);
                var alphaChannelValue = pixels[3]; // Layout of pixel is reverse ARGB: blue, green, red, alpha

                if (alphaChannelValue <= HitTestAlphaThreshold)
                    return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception occured during HitTesting. Info: " + ex);
            }

            return false;
        }

        private Point TranslateHitpointToBitmap(BitmapSource bmp, Point hitPoint)
        {
            var xFactor = bmp.PixelWidth / ActualWidth;
            var yFactor = bmp.PixelHeight / ActualHeight;

            return new Point(hitPoint.X * xFactor, hitPoint.Y * yFactor);
        }

        /* This method is the way to go when we want to let an event tunnel up or bubble down to the PARENT control of this: */
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            HitTestResult hitTestResult = base.HitTestCore(hitTestParameters);
            if (hitTestResult == null || EventTargetControl == null)
                return hitTestResult;

            return IsTransparentAtHitTestResultPoint(hitTestParameters.HitPoint, hitTestResult)
                ? null
                : hitTestResult;
        }
    }
}
