﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TestObservableCollection.ViewModels
{
   public enum CursorIndicatorDiamond
   {
      TypeGoingToBeReplacedDiamond
   }

   public class ItemViewModel : INotifyPropertyChanged
   {
      public ItemViewModel(double frame, BitmapSource cursorImageBitmap, byte r, byte g, byte b )
      {
         _keyframeFrame = frame;
         _cursorImageBitmap = cursorImageBitmap;
         MyBrush = new SolidColorBrush(new Color() { R = r, G = g, B = b, A = 255 } );
      }

      public CursorIndicatorDiamond CursorIndicatorDiamond
      {
         get
         {
            return CursorIndicatorDiamond.TypeGoingToBeReplacedDiamond;
         }
      }

      private BitmapSource _cursorImageBitmap;
      public BitmapSource CursorImageBitmap
      {
         get => _cursorImageBitmap;
      }

      public Brush MyBrush { get; }

      private double _keyframeFrame = 0;
      public double KeyframeFrame
      {
         get => _keyframeFrame;
      }

      //private double _zoomLevel = 1;
      //public double ZoomLevel
      //{
      //   get => _zoomLevel;
      //   set
      //   {
      //      if ( value == _zoomLevel )
      //         return;

      //      value = _zoomLevel;
      //      PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ZoomLevel ) ) );
      //   }
      //}

      public event PropertyChangedEventHandler PropertyChanged;
   }
}