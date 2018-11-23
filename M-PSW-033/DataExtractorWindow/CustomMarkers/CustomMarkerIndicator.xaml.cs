using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;
using DataExtractorWindow;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.ComponentModel;

namespace Demo.WindowsPresentation.CustomMarkers
{
   /// <summary>
   /// Interaction logic for CustomMarkerDemo.xaml
   /// </summary>
   public partial class CustomMarkerIndicator
   {
      Popup Popup;
      Label Label;
      GMapMarker Marker;
      MainWindow MainWindow;

      public CustomMarkerIndicator(MainWindow window, GMapMarker marker, double Heading, uint Zone, string title)
      {
        this.InitializeComponent();
        this.MainWindow = window;
        this.Marker = marker;

        Popup = new Popup();
        Label = new Label();

            RotateTransform IndicatorTransfrom = new RotateTransform(Heading);
            PathIndicator.LayoutTransform = IndicatorTransfrom;

            if (Zone == 1)
            {
                PathIndicator.Fill = Brushes.Blue;
            }
            else if (Zone == 2)
            {
                PathIndicator.Fill = Brushes.Yellow;
            }
            else if (Zone == 3)
            {
                PathIndicator.Fill = Brushes.Red;
            }
            else
            {
                PathIndicator.Fill = Brushes.Transparent;
            }

            //CustomMarkerAngle = (Double) (Heading + 180);

        this.SizeChanged += new SizeChangedEventHandler(CustomMarkerIndicator_SizeChanged);
        this.MouseEnter += new MouseEventHandler(CustomMarkerIndicator_MouseEnter);
        this.MouseLeave += new MouseEventHandler(CustomMarkerIndicator_MouseLeave);
    
        Popup.Placement = PlacementMode.Mouse;
        {
            Label.Background = Brushes.Gray;
            Label.Foreground = Brushes.White;
            Label.BorderBrush = Brushes.Black;
            Label.BorderThickness = new Thickness(2);
            Label.Padding = new Thickness(5);
            Label.FontSize = 12;
            Label.Content = title;
        }
        Popup.Child = Label;
      }
        private Double _CustomMarkerAngle;
        public Double CustomMarkerAngle
        {
            get { return _CustomMarkerAngle; }
            set
            {
                _CustomMarkerAngle = value;
                OnPropertyChanged("CustomMarkerAngle");
            }
        }

      void CustomMarkerIndicator_SizeChanged(object sender, SizeChangedEventArgs e)
      {
         Marker.Offset = new Point(-e.NewSize.Width/2, -e.NewSize.Height/2);
      }

      void CustomMarkerIndicator_MouseMove(object sender, MouseEventArgs e)
      {
         if(e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
         {
            Point p = e.GetPosition(MainWindow.MainMap);
            Marker.Position = MainWindow.MainMap.FromLocalToLatLng((int) p.X, (int) p.Y);
         }
      }

      void CustomMarkerIndicator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         if(!IsMouseCaptured)
         {
            Mouse.Capture(this);
         }
      }

      void CustomMarkerIndicator_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         if(IsMouseCaptured)
         {
            Mouse.Capture(null);
         }
      }

      void CustomMarkerIndicator_MouseLeave(object sender, MouseEventArgs e)
      {
         Marker.ZIndex -= 10000;
         Popup.IsOpen = false;
      }

      void CustomMarkerIndicator_MouseEnter(object sender, MouseEventArgs e)
      {
         Marker.ZIndex += 10000;
         Popup.IsOpen = true;
      }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}