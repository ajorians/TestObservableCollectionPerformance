using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace TestObservableCollection.CustomPanels
{
   public class KeyframePanel : Panel
   {
      // AttachedProperty used by items that wish to be drawn by this Panel
      public static readonly DependencyProperty KeyframeFrameProperty =
         DependencyProperty.RegisterAttached( "KeyframeFrame",
                                              typeof( long ),
                                              typeof( KeyframePanel ),
                                              new FrameworkPropertyMetadata( 0L, FrameworkPropertyMetadataOptions.AffectsParentMeasure ) );

      public static void SetKeyframeFrame( DependencyObject dependencyObject, long value ) => dependencyObject.SetValue( KeyframeFrameProperty, value );

      protected static long GetKeyframeFrame( DependencyObject element )
      {
         if ( element == null )
         {
            return 0;
         }

         // Grab the KeyframeTime attachedproperty.
         // If it can't find the property, it will be zero.
         return (long)element.GetValue( KeyframeFrameProperty );
      }

      public static readonly DependencyProperty ZoomLevelProperty =
         DependencyProperty.RegisterAttached( "ZoomLevel",
                                              typeof( double ),
                                              typeof( KeyframePanel ),
                                              new FrameworkPropertyMetadata( 0d, FrameworkPropertyMetadataOptions.AffectsParentMeasure ) );

      public static void SetZoomLevel( DependencyObject dependencyObject, double value ) => dependencyObject.SetValue( ZoomLevelProperty, value );

      protected static double GetZoomLevel( DependencyObject element )
      {
         if ( element == null )
         {
            return 0d;
         }

         // Grab the KeyframeTime attachedproperty.
         // If it can't find the property, it will be zero.
         return (double)element.GetValue( ZoomLevelProperty );
      }

      //public static readonly DependencyProperty IndicatorSizeProperty =
      //   DependencyProperty.RegisterAttached( "IndicatorSize",
      //                                        typeof( double ),
      //                                        typeof( KeyframePanel ),
      //                                        new FrameworkPropertyMetadata( 0d, FrameworkPropertyMetadataOptions.AffectsParentMeasure ) );

      //public static void SetIndicatorSize( DependencyObject dependencyObject, double value ) => dependencyObject.SetValue( IndicatorSizeProperty, value );

      //protected static double GetIndicatorSize( DependencyObject element )
      //{
      //   if ( element == null )
      //   {
      //      return 0d;
      //   }

      //   return (double)element.GetValue( IndicatorSizeProperty );
      //}

      // Measures itself and tells it children to measure themselves
      protected override Size MeasureOverride( Size availableSize )
      {
         Size panelDesiredSize = new Size();

         foreach ( UIElement child in InternalChildren )
         {
            child.Measure( availableSize );
            double zoomLevel = (double)child.GetValue( ZoomLevelProperty );
            panelDesiredSize.Width = child.DesiredSize.Width * InternalChildren.Count * zoomLevel;
            panelDesiredSize.Height = Math.Max( panelDesiredSize.Height, child.DesiredSize.Height);
         }

         return panelDesiredSize;
      }

      protected double GetKeyframePixelOffset( DependencyObject element )
      {
         if ( element == null )
         {
            return 0;
         }

         long keyframeFrame = (long)element.GetValue( KeyframeFrameProperty );
         double zoomLevel = (double)element.GetValue( ZoomLevelProperty );

         return keyframeFrame * zoomLevel;
      }

      // Arranges the children. Called after Measure.
      protected override Size ArrangeOverride( Size finalSize )
      {
         var orderedChildren = Children.Cast<UIElement>()
            .Where( element => element != null )
            .OrderByDescending( GetKeyframeFrame ).ToList();

         foreach ( UIElement element in orderedChildren )
         {
            double kefyrameFrameOffset = GetKeyframePixelOffset( element );

            // Calls the arrange for child and depending on orientation gives the rect where it needs to be.
            element.Arrange( new Rect( new Point( kefyrameFrameOffset, 0), element.DesiredSize ) );
         }
         return finalSize;
      }
   }
}
