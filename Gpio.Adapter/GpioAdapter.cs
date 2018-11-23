using Gpio.Abstract;
using Gpio.Implemention;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Gpio.Adapter
{
    public class GpioAdapter
    {

        public event EventHandler<PinEventArgs> PinSet;
        public short[] AvailiblePins { get; private set; }
        public bool FileWatchingListening
        {
            get
            {
                return _fileWatcher.EnableRaisingEvents;
            }
            set
            {
                _fileWatcher.EnableRaisingEvents = value;
            }
        }

        private DirectoryInfo _directory;
        private FileSystemWatcher _fileWatcher;
        private Dictionary<short, Pin> _pinKeyPairs;  
        private const string _fileName = "PinMappings.json";
        private string FileName
        {
            get
            {
                return Path.Combine(_directory.FullName, _fileName);
            }
        }

        public GpioAdapter(string directory) : this(directory, new short[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 })
        {    
        }

        public GpioAdapter(string directory, short[] availablePins)
        {
            AvailiblePins = availablePins;
            if (!Directory.Exists(directory)) throw new DirectoryNotFoundException(directory + " not found.");
            _directory = new DirectoryInfo(directory);
            _pinKeyPairs = GetPinDictionary();
            InitFileWatcher();
        }

        public IPin GetPin(short pinId)
        {
            return _pinKeyPairs[pinId];
        }

        public bool UpdatePins(IEnumerable<IPin> pins)
        {
            foreach(var pin in pins)
            {
                _pinKeyPairs[pin.PinNo] = pin as Pin;
            }
            UpdatePinFile(_pinKeyPairs);
            return true;
        }

        private void InitFileWatcher()
        {
            _fileWatcher = new FileSystemWatcher(_directory.FullName);
            _fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _fileWatcher.Filter = "PinMappings.json";
            _fileWatcher.Changed += HandleFileChanged;
        }
        
        private Dictionary<short, Pin> GetPinDictionary()
        {
            Dictionary<short, Pin> pins;
            if (!File.Exists(FileName))
            {
                pins = CreateNewPinDictionary();
                UpdatePinFile(pins);
            }
            else
            {
                pins = Serialization.DeSerialize(File.ReadAllText(FileName));
            }
            return pins;
        }

        private Dictionary<short, Pin> CreateNewPinDictionary()
        {
            var pins = new Dictionary<short, Pin>();
            foreach(var p in AvailiblePins)
            {
                Pin pin = new Pin()
                {
                    PinNo = p,
                    Direction = PinDirection.Out,
                    Value = PinValue.Low
                };
                pins.Add(p, pin);
            }
            return pins;
        }

        private void HandleFileChanged(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("File changed");
            var newPins = GetPinDictionary();

            foreach(var pin in FindDifferences(_pinKeyPairs, newPins))
            {
                PinSet?.Invoke(this, new PinEventArgs(pin));
            }

            _pinKeyPairs = newPins;
        }

        private List<Pin> FindDifferences(Dictionary<short, Pin> oldKeys, Dictionary<short, Pin> newKeys)
        {
            List<Pin> diffs = new List<Pin>();

            foreach(var oldPin in oldKeys.Values)
            {
                var newPin = newKeys[oldPin.PinNo];
                if(oldPin.Direction != newPin.Direction || oldPin.Value != newPin.Value)
                {
                    diffs.Add(newPin);
                }
            }
            return diffs;
        }

        private void UpdatePinFile(Dictionary<short, Pin> pins)
        {
            File.WriteAllText(FileName, Serialization.Serialize(pins));
        }
    }
}
