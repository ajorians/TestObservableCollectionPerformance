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

            int numItems = FullListOfItems.Count;
            for ( int i=0; i< numItems; i++ )
            {
               FullListOfItems[i].IsVisible = (i % _showNumber) == 0;
            }
            SyncUpItems();

            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ShowNumber ) ) );
         }
      }

      private int _imageChoice = 1;
      public int ImageChoice
      {
         get => _imageChoice;
         set
         {
            if ( value == _imageChoice )
               return;

            _imageChoice = value;

            int numItems = FullListOfItems.Count;
            for ( int i = 0; i < numItems; i++ )
            {
               var image = new BitmapImage( new Uri( $"pack://application:,,,/Images/test{_imageChoice}.png" ) );

               FullListOfItems[i].CursorImageBitmap = image;
            }

            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ImageChoice ) ) );
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

            var image = new BitmapImage( new Uri( "pack://application:,,,/Images/test1.png" ) );

            FullListOfItems.Clear();

            for ( int i = 0; i < _numIndicators; i++ )
            {
               double position = i* 40;
               byte b = (byte)( i*5 );

               //var image = new BitmapImage( new Uri( "pack://application:,,,/Images/test1.png" ) );

               bool isVisible = (i%ShowNumber)==0;
               FullListOfItems.Add( new ItemViewModel( position, image, isVisible, 255, 0, b) );
            }

            SyncUpItems();
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( NumIndicators ) ) );
         }
      }

      private void SyncUpItems()
      {
         var onlyVisibleItems = FullListOfItems.Where( item => item.IsVisible ).ToList();

         Items.Clear();
         Items.AddRange( onlyVisibleItems );
      }

      private List<ItemViewModel> FullListOfItems = new List<ItemViewModel>();

      public ObservableRangeCollection<ItemViewModel> Items
      {
         get;
      } = new ObservableRangeCollection<ItemViewModel>();

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
