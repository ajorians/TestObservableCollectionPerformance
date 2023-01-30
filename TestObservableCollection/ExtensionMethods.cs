using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Markup;

namespace TestObservableCollection
{
   public static class GeneralExtensionMethods
   {
      public static ObservableCollection<T> ToObservableCollection<T>( this IEnumerable<T> source )
      {
         if ( source == null )
         { throw new ArgumentNullException( nameof( source ) ); }
         return new ObservableCollection<T>( source );
      }

      public static void ForceToCount<T>( this ObservableCollection<T> source, int count ) where T : new()
      {
         if ( source == null )
         { throw new ArgumentNullException( nameof( source ) ); }

         while ( source.Count > count )
         { source.RemoveAt( source.Count - 1 ); }
         while ( source.Count < count )
         { source.Add( new T() ); }
      }

      public static bool IsWidthOrHeightEnclosedBy( this Rect rect, Rect enclosingRect )
      {
         if ( enclosingRect.IsEmpty )
            return false;

         if ( !rect.IntersectsWith( enclosingRect ) )
            return false;

         var intersectionResultRect = new Rect( rect.Size ) { Location = rect.Location };
         intersectionResultRect.Intersect( enclosingRect );

         return ( intersectionResultRect.Width == rect.Width
            || intersectionResultRect.Height == rect.Height );
      }

      public static T Clamp<T>( this T val, T min, T max ) where T : IComparable<T>
      {
         if ( min.CompareTo( max ) > 0 )
            throw new ArgumentException( ( "Minimum value is greater than maximum." ) );

         if ( val.CompareTo( min ) < 0 )
            return min;
         else if ( val.CompareTo( max ) > 0 )
            return max;
         else
            return val;
      }

      public static String ConvertToString( this Enum eff )
      {
         return Enum.GetName( eff.GetType(), eff );
      }

      public static int NumberOfDecimalPlaces( this double theDouble )
      {
         return BitConverter.GetBytes( decimal.GetBits( (decimal)theDouble )[3] )[2];
      }

      public static bool FitsIn( this System.Windows.Size theSize, System.Windows.Size hostSize )
      {
         var fitsInHostWidth = theSize.Width <= hostSize.Width;
         var fitsInHostHeight = theSize.Height <= hostSize.Height;
         return fitsInHostWidth && fitsInHostHeight;
      }

      public static System.Windows.Size ExpandBy( this System.Windows.Size theSize, double amount )
      {
         return theSize.ExpandBy( amount, amount );
      }

      public static System.Windows.Size ExpandBy( this System.Windows.Size theSize, double widthAmount, double heightAmount )
      {
         return new System.Windows.Size( theSize.Width + widthAmount, theSize.Height + heightAmount );
      }

      public static T XamlClone<T>( this T original )
         where T : class
      {
         if ( original == null )
            return null;

         object clone;
         using ( var stream = new MemoryStream() )
         {
            XamlWriter.Save( original, stream );
            stream.Seek( 0, SeekOrigin.Begin );
            clone = XamlReader.Load( stream );
         }

         if ( clone is T )
            return (T)clone;
         else
            return null;
      }

   }
}
