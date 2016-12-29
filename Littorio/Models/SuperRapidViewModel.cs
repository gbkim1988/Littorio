using Littorio.Async;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Littorio.Models
{
    public sealed class Query2WhoisViewModel
    {
        public Query2WhoisViewModel(SuperRapidViewModel parent, string query, IAsyncCommand command)
        {
            LoadingMessage = "Sending Query (" + query + ")...";
            Command = command;
            RemoveCommand = new DelegateCommand(() => parent.Operations.Remove(this));
        }

        public string LoadingMessage { get; private set; }

        public IAsyncCommand Command { get; private set; }

        public ICommand RemoveCommand { get; private set; }
    }

    public sealed class SuperRapidViewModel : INotifyPropertyChanged
    {
        ObservableCollection<UserXmlModel> datagrid = new ObservableCollection<UserXmlModel>();
        public SuperRapidViewModel()
        {
            Query = "61.1.1.111";
            Operations = new ObservableCollection<Query2WhoisViewModel>();

            Query2WhoisCommand = new DelegateCommand(() => 
            {
                // AsyncCommand 를 생성
                
            });
        }

        private string _query;
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Query2WhoisViewModel> Operations { get; private set; }

        public ICommand Query2WhoisCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyname = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
