using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TestObservableCollectionVsDrawing;

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

      private int _showNumber = 1;
      public int ShowNumber
      {
         get => _showNumber;
         set
         {
            if ( value == _showNumber )
               return;

            _showNumber = value;

            int numItems = Items.Count;
            for ( int i=0; i< numItems; i++ )
            {
               Items[i].IsVisible = (i % _showNumber) == 0;
            }

            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ShowNumber ) ) );
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

            var image = new BitmapImage( new Uri( "pack://application:,,,/Images/test1.png" ) );

            List<ItemViewModel> newItems = new List<ItemViewModel>( _numIndicators );

            for ( int i = 0; i < _numIndicators; i++ )
            {
               double position = i* 40;
               byte b = (byte)( i*5 );

               //var image = new BitmapImage( new Uri( "pack://application:,,,/Images/test1.png" ) );

               bool isVisible = (i%ShowNumber)==0;
               newItems.Add( new ItemViewModel( position, image, isVisible, 255, 0, b) );
            }

            Items.AddRange( newItems );
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( NumIndicators ) ) );
         }
      }

      private ObservableRangeCollection<ItemViewModel> _items = new ObservableRangeCollection<ItemViewModel>();

      public ObservableRangeCollection<ItemViewModel> Items
      {
         get => _items;
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
