﻿using Sensus.DataStores.Local;
using Sensus.DataStores.Remote;
using Sensus.Probes;
using Sensus.UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sensus
{
    /// <summary>
    /// Defines a Sensus protocol.
    /// </summary>
    public class Protocol : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private List<Probe> _probes;
        private bool _running;
        private LocalDataStore _localDataStore;
        private RemoteDataStore _remoteDataStore;
        private PropertyChangedEventHandler _notifyWatchersOfProbesChange;

        [StringUiProperty("Name:", true)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (!value.Equals(_name, StringComparison.Ordinal))
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Probe> Probes
        {
            get { return _probes; }
        }

        [BooleanUiProperty("Status:", true)]
        public bool Running
        {
            get { return _running; }
            set
            {
                if (value != _running)
                {
                    _running = value;
                    OnPropertyChanged();

                    if (_running)
                        App.Get().SensusService.StartProtocol(this);
                    else
                        App.Get().SensusService.StopProtocol(this);
                }
            }
        }

        public LocalDataStore LocalDataStore
        {
            get { return _localDataStore; }
            set
            {
                if (value != _localDataStore)
                {
                    _localDataStore = value;
                    OnPropertyChanged();
                }
            }
        }

        public RemoteDataStore RemoteDataStore
        {
            get { return _remoteDataStore; }
            set
            {
                if (value != _remoteDataStore)
                {
                    _remoteDataStore = value;
                    OnPropertyChanged();
                }
            }
        }

        public Protocol(string name, bool addAllProbes)
        {
            _name = name;
            _probes = new List<Probe>();
            _running = false;

            _notifyWatchersOfProbesChange = (o, e) =>
                {
                    OnPropertyChanged("Probes");
                };

            if (addAllProbes)
                foreach (Probe probe in Probe.GetAll())
                    AddProbe(probe);
        }

        public void AddProbe(Probe probe)
        {
            probe.Protocol = this;
            probe.PropertyChanged += _notifyWatchersOfProbesChange;

            _probes.Add(probe);
        }

        public void RemoveProbe(Probe probe)
        {
            probe.Protocol = null;
            probe.PropertyChanged -= _notifyWatchersOfProbesChange;

            _probes.Remove(probe);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartAsync()
        {
            if (Logger.Level >= LoggingLevel.Normal)
                Logger.Log("Initializing and starting probes for protocol " + _name + ".");

            int probesStarted = 0;
            foreach (Probe probe in _probes)
                if (probe.Enabled && probe.InitializeAndStartAsync())
                    probesStarted++;

            if (probesStarted > 0)
            {
                if (Logger.Level >= LoggingLevel.Normal)
                    Logger.Log("Starting local data store.");

                try
                {
                    _localDataStore.StartAsync(this);

                    if (Logger.Level >= LoggingLevel.Normal)
                        Logger.Log("Local data store started.");
                }
                catch (Exception ex)
                {
                    if (Logger.Level >= LoggingLevel.Normal)
                        Logger.Log("Local data store failed to start:  " + ex.Message + Environment.NewLine + ex.StackTrace);

                    Running = false;
                    return;
                }

                if (Logger.Level >= LoggingLevel.Normal)
                    Logger.Log("Starting remote data store.");

                try
                {
                    _remoteDataStore.StartAsync(_localDataStore);

                    if (Logger.Level >= LoggingLevel.Normal)
                        Logger.Log("Remote data store started.");
                }
                catch (Exception ex)
                {
                    if (Logger.Level >= LoggingLevel.Normal)
                        Logger.Log("Remote data store failed to start:  " + ex.Message);

                    Running = false;
                    return;
                }
            }
            else
            {
                if (Logger.Level >= LoggingLevel.Normal)
                    Logger.Log("No probes were started.");

                Running = false;
            }
        }

        public void StopAsync()
        {
            if (Logger.Level >= LoggingLevel.Normal)
                Logger.Log("Stopping probes.");

            foreach (Probe probe in _probes)
                if (probe.Controller.Running)
                    try { probe.Controller.StopAsync(); }
                    catch (Exception ex) { if (Logger.Level >= LoggingLevel.Normal) Logger.Log("Failed to stop " + probe.Name + "'s controller:  " + ex.Message + Environment.NewLine + ex.StackTrace); }

            if (_localDataStore != null && _localDataStore.Running)
            {
                if (Logger.Level >= LoggingLevel.Normal)
                    Logger.Log("Stopping local data store.");

                try { _localDataStore.StopAsync(); }
                catch (Exception ex) { if (Logger.Level >= LoggingLevel.Normal) Logger.Log("Failed to stop local data store:  " + ex.Message + Environment.NewLine + ex.StackTrace); }
            }

            if (_remoteDataStore != null && _remoteDataStore.Running)
            {
                if (Logger.Level >= LoggingLevel.Normal)
                    Logger.Log("Stopping remote data store.");

                try { _remoteDataStore.StopAsync(); }
                catch (Exception ex) { if (Logger.Level >= LoggingLevel.Normal) Logger.Log("Failed to stop remote data store:  " + ex.Message + Environment.NewLine + ex.StackTrace); }
            }
        }

        public void ClearPropertyChangedDelegates()
        {
            PropertyChanged = null;

            foreach (Probe probe in _probes)
                probe.ClearPropertyChangedDelegates();

            if (_localDataStore != null)
                _localDataStore.ClearPropertyChangedDelegates();

            if (_remoteDataStore != null)
                _remoteDataStore.ClearPropertyChangedDelegates();
        }
    }
}
