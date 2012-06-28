using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace AchievementManager.Controls
{
    public class ProgressControl : Control
    {
        //////////////////////////////////
        //
        // [VARIABLES]
        //
        //////////////////////////////////

        private static Pen DEFAULT_PEN = new Pen(Brushes.Black, 2.0);
        private static Pen TRANSPARENT_PEN = new Pen(Brushes.Transparent, 0);
        private static Typeface typeFace = new Typeface(new FontFamily("Arial"), new FontStyle(), new FontWeight(), new FontStretch());
        private static CultureInfo cultureInfo = new CultureInfo("EN-us");

        private double width = 0;
        private FormattedText ft;

        //////////////////////////////////
        //
        // [DEPENDENCY PROPERTIES]
        //
        //////////////////////////////////

        public static readonly DependencyProperty PercentProperty = DependencyProperty.Register(
            "Percent", typeof(double), typeof(ProgressControl), new PropertyMetadata((double)0));

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            "IsReadOnly", typeof(bool), typeof(ProgressControl), new PropertyMetadata(true));

        //////////////////////////////////
        //
        // [PROPERTIES]
        //
        //////////////////////////////////

        public double Percent 
        {
            get
            {
                return (double)this.GetValue(PercentProperty);
            }
            set
            {
                this.SetValue(PercentProperty, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (bool)this.GetValue(IsReadOnlyProperty);
            }
            set
            {
                this.SetValue(IsReadOnlyProperty, value);
            }
        }

        //////////////////////////////////
        //
        // [CONSTRUCTOR]
        //
        //////////////////////////////////

        static ProgressControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressControl), new FrameworkPropertyMetadata(typeof(ProgressControl)));
        }

        //////////////////////////////////
        //
        // [MEMBERS]
        //
        //////////////////////////////////

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == PercentProperty)
            {
                InvalidateVisual();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            width = (ActualWidth-(2*DEFAULT_PEN.Thickness)) * Percent;

            drawingContext.DrawRectangle(Brushes.Transparent, DEFAULT_PEN, new Rect(0, 0, ActualWidth, ActualHeight));

            drawingContext.DrawRectangle(Brushes.LightGreen, TRANSPARENT_PEN, new Rect(DEFAULT_PEN.Thickness,
                DEFAULT_PEN.Thickness, width, ActualHeight - (2 * DEFAULT_PEN.Thickness)));

            ft = new FormattedText((Percent*100).ToString() + " %", cultureInfo, FlowDirection.LeftToRight, typeFace, 20.0, Brushes.Black);

            drawingContext.DrawText(ft, new Point(20, 20));
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!IsReadOnly)
            {
                Update(e.GetPosition(this).X);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!IsReadOnly)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Update(e.GetPosition(this).X);
                }
            }
        }

        private void Update(double MouseX)
        {
            double p = (MouseX / ActualWidth) * 100;
            p = (Math.Round(p/10))*10;
            p /= 100;
            Percent = p;
            InvalidateVisual();
        }
    }
}
