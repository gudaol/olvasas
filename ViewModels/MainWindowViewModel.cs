using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OlvasasGyakorlo.Models;
using ReactiveUI;

namespace OlvasasGyakorlo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Output { get; set; } = "Üdv az alkalmazásban!";

        private string input = "";
        private string _length = "10";

        public string Length
        {
            get { return _length; }
            set
            {
                this._length = value;
                try
                {
                    int hossz = int.Parse(Length);
                    Output = Process.GetUpdatedOutput(this.input.ToLower(), hossz);
                }
                catch (Exception)
                {
                    Output = Process.GetUpdatedOutput(this.input.ToLower(), 10);
                }
                this.RaisePropertyChanged("Output");
            }
        }

        public string Input
        {
            get => input;
            set
            {
                input = new string((input + value).Distinct().ToArray()); try
                {
                    int hossz = int.Parse(Length);
                    Output = Process.GetUpdatedOutput(this.input.ToLower(), hossz);
                }
                catch (Exception)
                {
                    Output = Process.GetUpdatedOutput(this.input.ToLower(), 10);
                }
                this.RaisePropertyChanged("Output");
            }
        }
    }
}
