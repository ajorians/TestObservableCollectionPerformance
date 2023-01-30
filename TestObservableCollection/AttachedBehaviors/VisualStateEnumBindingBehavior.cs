
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace TestObservableCollection.AttachedBehaviors
{
   public class VisualStateEnumBindingBehavior : Behavior<FrameworkElement>
   {
      private FrameworkElement _attachedElement;

      protected override void OnAttached()
      {
         _attachedElement = AssociatedObject;
         _attachedElement.Loaded += _attachedElement_Loaded;
      }

      private void _attachedElement_Loaded( object sender, RoutedEventArgs e )
      {
         UpdateVisualState( EnumCondition );
      }

      private void UpdateVisualState( Enum condition )
      {
         if ( _attachedElement == null || condition == null )
            return;

         var enumTypeString = condition.GetType().Name;

         // The MSDN documentation [http://msdn.microsoft.com/en-us/library/system.windows.visualstatemanager.gotoelementstate.aspx]
         // states that you need to:
         //   "Call the GoToElementState method to change states on an element outside of a ControlTemplate
         //    (for example, if you use a VisualStateManager in a DataTemplate or Window). Call the GoToState
         //    method if you are changing states in a control that uses the VisualStateManager in its ControlTemplate."
         if ( ControlTemplateElement )
         {
            VisualStateManager.GoToState( _attachedElement, enumTypeString + condition.ConvertToString(), false );
         }
         else
         {
            VisualStateManager.GoToElementState( _attachedElement, enumTypeString + condition.ConvertToString(), true );
         }
      }

      #region Dependency Properties

      public static readonly DependencyProperty ControlTemplateElementProperty =
                  DependencyProperty.Register( nameof( ControlTemplateElement ),
                                               typeof( bool ),
                                               typeof( VisualStateEnumBindingBehavior ),
                                               new PropertyMetadata( false ) );
      public bool ControlTemplateElement
      {
         get { return (bool)GetValue( ControlTemplateElementProperty ); }
         set { SetValue( ControlTemplateElementProperty, value ); }
      }

      public static readonly DependencyProperty EnumConditionProperty =
            DependencyProperty.Register( nameof( EnumCondition ),
                                         typeof( Enum ),
                                         typeof( VisualStateEnumBindingBehavior ),
                                         new FrameworkPropertyMetadata( null, OnEnumConditionChanged, CoerceEnumCondition ) );
      public Enum EnumCondition
      {
         get { return (Enum)GetValue( EnumConditionProperty ); }
         set { SetValue( EnumConditionProperty, value ); }
      }
      private static void OnEnumConditionChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
      {
      }
      private static object CoerceEnumCondition( DependencyObject sender, object value )
      {
         ( (VisualStateEnumBindingBehavior)sender ).UpdateVisualState( (Enum)value );
         return value;
      }

      #endregion Dependency Properties
   }
}
