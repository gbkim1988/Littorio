using Littorio.Async;
using Littorio.Serivces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public SuperRapidViewModel()
        {
            Query = "61.1.1.111";
            Operations = new AsyncObservableCollection<Query2WhoisViewModel>();
            WhoisGrid = new AsyncObservableCollection<user>();
            Query2WhoisCommand = new DelegateCommand(() => 
            {
                // AsyncCommand 를 생성
                var countBytes = AsyncCommand.Create(token => ArmamentServices.QueryAndConvertToXmlAsync(Query, WhoisGrid, token));
                countBytes.Execute(null);
                Operations.Add(new Query2WhoisViewModel(this, Query, countBytes));

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

        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames) { }
                    //lbFiles.Items.Add(Path.GetFileName(filename));
            }
        }

        public AsyncObservableCollection<user> WhoisGrid { get; set; }
        public AsyncObservableCollection<Query2WhoisViewModel> Operations { get; private set; }

        public ICommand Query2WhoisCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyname = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
