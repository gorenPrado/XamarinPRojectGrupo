using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinCalendarProject.CustomsControls
{
    public class CustomViewCellControl : ViewCell
    {
        //DEBEMOS CREAR UNA PROPIEDAD BINDABLE PARA PODER ESTABLECER EL COLOR DE
        //FONDO EN NUESTRO CONTROL LISTVIEW
        //LOS VALORES QUE NOS PIDE SON EL NOMBRE DE LA PROPIEDAD (TERMINADO EN PROPERTY)
        //TIPO DE PROPIEDAD (COLOR), SOBRE QUE OBJETO Y UN VALOR POR DEFECTO
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
            BindableProperty.Create("SelectedItemBackgroundColor"
                , typeof(Color), typeof(CustomViewCellControl), Color.White);
        //AQUI REALIZAMOS LA ACCION PARA LA PROPIEDAD EN XAML
        public Color SelectedItemBackgroundColor
        {
            get
            {
                return (Color)GetValue(SelectedItemBackgroundColorProperty);
            }
            set
            {
                SetValue(SelectedItemBackgroundColorProperty, value);
            }
        }
    }
}
