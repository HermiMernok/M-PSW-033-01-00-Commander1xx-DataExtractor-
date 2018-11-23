using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtractorWindow
{
    public class ProximityZoneInfo : INotifyPropertyChanged
    {
        private CriticalZoneInfo _CrticicalZone;
        public CriticalZoneInfo CrticicalZone
        {
            get
            {
                int Index = 0;
                try
                {                 
                        Index = _BrakeDistanceSpeed.IndexOf(_BrakeDistanceSpeed.FindLast(x => (x <= _CrticicalZone.SpeedKMH)));
                }
                catch
                {
                   
                }

                if (Index < 0)
                {
                    _CrticicalZone.BrakeDistanceLower = 0;
                    _CrticicalZone.BrakeDistanceUpper = _BrakeDistance.ElementAt(0);
                }
                else if (Index < (_BrakeDistance.Count-1) )
                {
                   
                        if (_BrakeDistanceSpeed.ElementAt(Index + 1) - _BrakeDistanceSpeed.ElementAt(Index) == 10)
                        {
                            _CrticicalZone.BrakeDistanceLower = _BrakeDistance.ElementAt(Index);
                            _CrticicalZone.BrakeDistanceUpper = _BrakeDistance.ElementAt(Index + 1);
                        }
                        else
                        {
                            _CrticicalZone.BrakeDistanceLower = _BrakeDistance.ElementAt(Index + 1);
                            _CrticicalZone.BrakeDistanceUpper = _BrakeDistance.ElementAt(Index + 1);
                        }                    

                }
                else
                {
                    _CrticicalZone.BrakeDistanceLower = 60;
                    _CrticicalZone.BrakeDistanceUpper = 60;                     
                }
                      
                        int test = 0;

                if(_CrticicalZone.SpeedKHMChanged)
                {
                    _WarningZone.SpeedKMH = _CrticicalZone.SpeedKMH;
                    _PresenceZone.SpeedKMH = _CrticicalZone.SpeedKMH;
                    _CrticicalZone.SpeedKHMChanged = false;
                }

                if(_CrticicalZone.PDSTimeChanged)
                {
                    _WarningZone.PDSTime = _CrticicalZone.PDSTime;
                    _PresenceZone.PDSTime = _CrticicalZone.PDSTime;
                    _CrticicalZone.PDSTimeChanged = false;
                }

                OnPropertyChanged("WarningZone");
                _CrticicalZone.UpdateCriticalDistance();
                return _CrticicalZone;
            }
            set
            {
                if (_CrticicalZone != value)
                {
                    _CrticicalZone = value;
                    OnPropertyChanged("CrticicalZoneList");
                }
            }

        }

        private GeneralZoneInfo _WarningZone;
        public GeneralZoneInfo WarningZone
        {
            get
            {
                _WarningZone.AlertDistance = _WarningZone.ReactionDistance + _CrticicalZone.CriticalDistance;
                
                OnPropertyChanged("PresenceZone");
                return _WarningZone;
            }
            set
            {

                if (_WarningZone != value)
                {
                    _WarningZone = value;
                    OnPropertyChanged("WarningZone");
                }
            }
        }

        private GeneralZoneInfo _PresenceZone;
        public GeneralZoneInfo PresenceZone
        {
            get {
                _PresenceZone.AlertDistance = _WarningZone.AlertDistance + _PresenceZone.ReactionDistance;
                return _PresenceZone;
            }
            set
            {
                if (_PresenceZone != value)
                {
                    _PresenceZone = value;
                  
                    OnPropertyChanged("PresenceZone");
                }
            }
        }

        private List<decimal> _BrakeDistanceSpeed;
        public List<decimal> BrakeDistanceSpeed
        {
            get
            {
               
                return _BrakeDistanceSpeed;
            }
            set
            {
                if (_BrakeDistanceSpeed != value)
                {
                    _BrakeDistanceSpeed = value;

                    OnPropertyChanged("BrakeDistanceSpeed");
                }
            }
        }

        private List<decimal> _BrakeDistance;
        public List<decimal> BrakeDistance
        {
            get
            {
              
                return _BrakeDistance;
            }
            set
            {
                if (_BrakeDistance != value)
                {
                    _BrakeDistance = value;

                    OnPropertyChanged("BrakeDistance");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public class CriticalZoneInfo : INotifyPropertyChanged
    {


        public void UpdateCriticalDistance()
        {
            OnPropertyChanged("CriticalDistance");
        }



        private bool _SpeedKHMChanged;
        public bool SpeedKHMChanged
        {
            get { return _SpeedKHMChanged; }
            set
            {
                if (_SpeedKHMChanged != value)
                {
                    _SpeedKHMChanged = value;
                }
            }
        }

        private decimal _SpeedKMH;
        public decimal SpeedKMH
        {
            get { return _SpeedKMH; }
            set
            {
                if (_SpeedKMH != value)
                {
                    _SpeedKMH = value;
                    _SpeedKHMChanged = true;
                    OnPropertyChanged("SpeedKMH");
                    OnPropertyChanged("SpeedMS");
                    OnPropertyChanged("BrakeDistanceCalc");
                    OnPropertyChanged("ReactionDistance");
                }
            }
        }

        private decimal _SpeedMS;
        public decimal SpeedMS
        {
            get
            {
                _SpeedMS = _SpeedKMH / 3.6m;
                return Math.Round(_SpeedMS, 3);
            }
            set
            {
                if (_SpeedMS != value)
                {
                    _SpeedMS = value;
                    OnPropertyChanged("SpeedMS");
                }
            }
        }

        private decimal _BrakeDistanceUpper;
        public decimal BrakeDistanceUpper
        {
       
            get { return _BrakeDistanceUpper; }
            set
            {
                if (_BrakeDistanceUpper != value)
                {
                    _BrakeDistanceUpper = value;
                    OnPropertyChanged("BrakeDistanceCalc");
                    OnPropertyChanged("BrakeDistanceUpper");
                }
            }
        }

        private decimal _BrakeDistanceLower;
        public decimal BrakeDistanceLower
        {
            get { return _BrakeDistanceLower; }
            set
            {
                if (_BrakeDistanceLower != value)
                {
                    _BrakeDistanceLower = value;
                    OnPropertyChanged("BrakeDistanceCalc");
                    OnPropertyChanged("BrakeDistanceLower");
                }
            }
        }

        private decimal _BrakeDistanceCalc;
        public decimal BrakeDistanceCalc
        {
            get
            {
                _BrakeDistanceCalc = BrakeDistanceLower + ((_BrakeDistanceUpper - BrakeDistanceLower) / 10) * (Math.Floor(_SpeedKMH) % 10); 
                //  OnPropertyChanged("BrakeDistanceCalc");
                return Math.Round(_BrakeDistanceCalc, 3);
            }
          

        }

        private UInt32 _PDSTime;
        public UInt32 PDSTime
        {
            get { return _PDSTime; }
            set
            {
                if (_PDSTime != value)
                {
                    _PDSTime = value;
                    _PDSTimeChanged = true;
                    OnPropertyChanged("PDSTime");
                    OnPropertyChanged("ReactionDistance");
                }
            }
        }

        private bool _PDSTimeChanged = false;
        public bool PDSTimeChanged
        {
            get { return _PDSTimeChanged; }
            set
            {
                if (_PDSTimeChanged != value)
                {
                    _PDSTimeChanged = value;
                }
            }
        }

        private UInt32 _COMTime;
        public UInt32 COMTime
        {
            get { return _COMTime; }
            set
            {
                if (_COMTime != value)
                {
                    _COMTime = value;
                    OnPropertyChanged("COMTime");
                    OnPropertyChanged("ReactionDistance");
                }
            }
        }

        private UInt32 _OEMTime;
        public UInt32 OEMTime
        {
            get { return _OEMTime; }
            set
            {
                if (_OEMTime != value)
                {
                    _OEMTime = value;
                    OnPropertyChanged("OEMTime");
                    OnPropertyChanged("ReactionDistance");

                }
            }
        }

        private decimal _ReactionDistance;
        public decimal ReactionDistance
        {
            get
            {
                _ReactionDistance = ((PDSTime + COMTime + OEMTime) * SpeedMS) / 1000;

                OnPropertyChanged("CriticalDistance");
                return Math.Round(_ReactionDistance, 3);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private decimal _VehicleLength;
        public decimal VehicleLength
        {
            get { return _VehicleLength; }
            set
            {
                if (_VehicleLength != value)
                {
                    _VehicleLength = value;
                    OnPropertyChanged("VehicleLength");
                    OnPropertyChanged("CriticalDistance");
                }
            }
        }

        private decimal _ProhibitZoneDistance;
        public decimal ProhibitZoneDistance
        {
            get { return _ProhibitZoneDistance; }
            set
            {
                if (_ProhibitZoneDistance != value)
                {
                    _ProhibitZoneDistance = value;
                    OnPropertyChanged("ProhibitZoneDistance");
                    OnPropertyChanged("CriticalDistance");
                }
            }
        }

        private decimal _CriticalDistance;
        public decimal CriticalDistance
        {
            get
            {
                _CriticalDistance = _BrakeDistanceCalc + _ReactionDistance + (_VehicleLength / 2) + _ProhibitZoneDistance;
                return Math.Round(_CriticalDistance, 3);
            }
        }
        
        
        private decimal _MeasuredDistance;
        public decimal MeasuredDistance
        {
            get { return _MeasuredDistance; }
            set
            {
                if (_MeasuredDistance != value)
                {
                    _MeasuredDistance = value;
                    OnPropertyChanged("MeasuredDistance");
                    OnPropertyChanged("CriticalDistanceErrorM");
                }
            }
        }

        private decimal _CriticalDistanceErrorM;
        public decimal CriticalDistanceErrorM
        {
            get
            {
                _CriticalDistanceErrorM = Math.Abs(_CriticalDistance - _MeasuredDistance);
                OnPropertyChanged("CriticalDistanceErrorP");
                return Math.Round(Math.Abs((_CriticalDistance - _MeasuredDistance)), 3);
            }

        }

        private decimal _CriticalDistanceErrorP;
        public decimal CriticalDistanceErrorP
        {
            get
            {
                _CriticalDistanceErrorP = (_CriticalDistanceErrorM / _CriticalDistance) * 100;

                return Math.Round(_CriticalDistanceErrorP, 3);
            }


        }
    }

    public class GeneralZoneInfo : INotifyPropertyChanged
    {
        private decimal _SpeedKMH;
        public decimal SpeedKMH
        {
            get { return _SpeedKMH; }
            set
            {
                if (_SpeedKMH != value)
                {
                    _SpeedKMH = value;                  
                    OnPropertyChanged("SpeedKMH");
                    OnPropertyChanged("SpeedMS");          
                    OnPropertyChanged("ReactionDistance");
                }
            }
        }


        private decimal _SpeedMS;
        public decimal SpeedMS
        {
            get
            {
                _SpeedMS = _SpeedKMH / 3.6m;         
                return Math.Round(_SpeedMS, 3);
            }
            set
            {
                if (_SpeedMS != value)
                {
                    _SpeedMS = value;
                    OnPropertyChanged("SpeedMS");
              
                }
            }
        }

        private UInt32 _OperatorTime;
        public UInt32 OperatorTime
        {
            get { return _OperatorTime; }
            set
            {
                if (_OperatorTime != value)
                {
                    _OperatorTime = value;
                    OnPropertyChanged("OperatorTime");
                    OnPropertyChanged("ReactionDistance");

                }
            }
        }

        private UInt32 _PDSTime;
        public UInt32 PDSTime
        {
            get { return _PDSTime; }
            set
            {
                if (_PDSTime != value)
                {
                    _PDSTime = value;
                    OnPropertyChanged("PDSTime");
                    OnPropertyChanged("ReactionDistance");
                }
            }
        }

        private decimal _ReactionDistance;
        public decimal ReactionDistance
        {
            get
            {
                _ReactionDistance = ((OperatorTime + PDSTime) * SpeedMS) / 1000;
                OnPropertyChanged("AlertDistance");
                return Math.Round(_ReactionDistance, 3);
            }
        }

        private decimal _AlertDistance;
        public decimal AlertDistance
        {
            get { return _AlertDistance; }
            set
            {
                if (_AlertDistance != value)
                {
                    _AlertDistance = value;
                    OnPropertyChanged("AlertDistance");
                    OnPropertyChanged("DistanceErrorM");
                }
            }
        }

        private decimal _MeasuredDistance;
        public decimal MeasuredDistance
        {
            get { return _MeasuredDistance; }
            set
            {
                if (_MeasuredDistance != value)
                {
                    _MeasuredDistance = value;
                    OnPropertyChanged("MeasuredDistance");
                    OnPropertyChanged("DistanceErrorM");
                }
            }
        }

        private decimal _DistanceErrorM;
        public decimal DistanceErrorM
        {
            get
            {
                _DistanceErrorM = Math.Abs(_AlertDistance - _MeasuredDistance);
                OnPropertyChanged("DistanceErrorP");
                return Math.Round(_DistanceErrorM, 3);
            }

        }

        private decimal _DistanceErrorP;
        public decimal DistanceErrorP
        {
            get
            {
                _DistanceErrorP = (_DistanceErrorM / _AlertDistance) * 100;

                return Math.Round(_DistanceErrorP, 3);
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }

  


}

