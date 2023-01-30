using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestObservableCollection.ViewModels
{
   public class VM : INotifyPropertyChanged
   {
      public VM()
      {
         NumIndicators = 70;
      }

      private double _zoomLevel = 1;
      public double ZoomLevel
      {
         get => _zoomLevel;
         set
         {
            if ( value == _zoomLevel )
               return;

            _zoomLevel = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ZoomLevel ) ) );
         }
      }

      private double _indicatorSize = 30;
      public double IndicatorSize
      {
         get => _indicatorSize;
         set
         {
            if ( value == _indicatorSize )
               return;

            _indicatorSize = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( IndicatorSize ) ) );
         }
      }

      private int _numIndicators = 0;
      public int NumIndicators
      {
         get => _numIndicators;
         set
         {
            if ( value == _numIndicators )
               return;

            _numIndicators = value;
            _items.Clear();
            for ( int i = 0; i < _numIndicators; i++ )
            {
               double position = i* 40;
               byte b = (byte)( i*5 );

               var image = new BitmapImage( new Uri( "pack://application:,,,/Images/test1.png" ) );

               _items.Add( new ItemViewModel( position, image, 255, 0, b) );
            }

            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( Items ) ) );
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( NumIndicators ) ) );
         }
      }

      private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();

      public ObservableCollection<ItemViewModel> Items
      {
         get => _items;
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
