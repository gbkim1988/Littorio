using Littorio.Async;
using Littorio.Serivces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            Query = "125.209.222.141";
            Operations = new AsyncObservableCollection<Query2WhoisViewModel>();
            WhoisGrid = new AsyncObservableCollection<WhoisAPNIC>();
            Query2WhoisCommand = new DelegateCommand(() => 
            {
                // AsyncCommand 를 생성
                var countBytes = AsyncCommand.Create(token => ArmamentServices.QueryAndConvertToXmlAsync(Query, WhoisGrid, token));
                countBytes.Execute(null);
                Operations.Add(new Query2WhoisViewModel(this, Query, countBytes));

            });

            File2WhoisCommand = new DelegateCommand(() =>
                {
                    // File Open and Execute Massive Whois Command
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    //openFileDialog.Multiselect = true;
                    openFileDialog.Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (openFileDialog.ShowDialog() == true)
                    {
                        this.FilePath = Path.GetFullPath(openFileDialog.FileName);
                        // List<string> 의 모든 요소를 제거 후 파일의 내용을 읽어옴
                        csvIP.Clear();
                        // LogParser를 통해 IP 와 Count 를 출력하였을 경우를 기준으로 전개한다.
                        using (var rd = new StreamReader(openFileDialog.FileName))
                        {
                            while (!rd.EndOfStream)
                            {
                                var splits = rd.ReadLine().Split(',');
                                Match match = Regex.Match(splits[0], @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                                if (match.Success)
                                {
                                    this.csvIP.Add(splits[0]);
                                }
                                
                            }
                        }
                    }
                }
            );

            FireWhoisQuery = new DelegateCommand(() => {
                // csvIP 의 라인수를 체크를 하고 로드
                if (this.csvIP.Count > 0)
                {
                    csvIP.ForEach(delegate (string ip) {
                        // AsyncCommand 를 생성 및 QueryAndConertToXmlAsync 비동기 메서드를 호출 후 해당 항목을 List 에서 삭제
                        var countBytes = AsyncCommand.Create(token => ArmamentServices.QueryAndConvertToXmlAsyncSemaphore(ip, WhoisGrid, limitConnection, token));
                        countBytes.Execute(null);
                        Operations.Add(new Query2WhoisViewModel(this, Query, countBytes));
                    });
                }
            });
        }
        // 동시 연결 수 제한
        SemaphoreSlim limitConnection = new SemaphoreSlim(5);
        private List<string> csvIP = new List<string>();
        private string _filePath;
        public string FilePath
        {
            get
            {
                return this._filePath;
            }
            set
            {
                this._filePath = value;
                OnPropertyChanged();
            }
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

        public AsyncObservableCollection<WhoisAPNIC> WhoisGrid { get; set; }
        public AsyncObservableCollection<Query2WhoisViewModel> Operations { get; private set; }

        public ICommand Query2WhoisCommand { get; private set; }
        public ICommand File2WhoisCommand { get; private set; }
        public ICommand FireWhoisQuery { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyname = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
