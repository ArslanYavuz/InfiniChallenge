using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfiniDemo.Behaviours
{
    public class SearchTextChangedBehaviour : Behavior<SearchBar>
    {
        public static int LastTextLength = 0;

        protected override void OnAttachedTo(Xamarin.Forms.SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Xamarin.Forms.SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue != null)
            {
                if (e.NewTextValue.Length >= 4 || e.NewTextValue.Length == 0)
                    ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);

                LastTextLength = e.NewTextValue.Length;
            }
          
        }
    }
}
