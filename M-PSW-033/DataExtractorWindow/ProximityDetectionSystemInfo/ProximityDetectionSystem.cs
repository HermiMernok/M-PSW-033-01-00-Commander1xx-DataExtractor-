using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProximityDetectionSystemInfo
{
    public enum PDS_EventID
    {
        [Category("Unused Events")]
        [Display(Name = "Unused Event 00")]
        [Description("This event cannot occur.")]
        None = 0,
        
        [Category("System Events")]
        [Display(Name = "Power Down")]
        [Description("This event occurs when the unit is powered down.")]
        Power_Down = 1,

        [Category("System Events")]
        [Display(Name = "Power Up")]
        [Description("This event occurs when the unit is powered up.")]
        Power_Up = 2,

        [Category("System Events")]
        [Display(Name = "Commissioning Required")]
        [Description("This event occurs when the unit is powered up and have not yet been configured.")]
        Commissioning_Required = 3,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 04")]
            [Description("This event cannot occur.")]
            Unused_Event_04 = 4,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 05")]
            [Description("This event cannot occur.")]
            Unused_Event_05 = 5,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 06")]
            [Description("This event cannot occur.")]
            Unused_Event_06 = 6,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 07")]
            [Description("This event cannot occur.")]
            Unused_Event_07 = 7,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 08")]
            [Description("This event cannot occur.")]
            Unused_Event_08 = 8,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 09")]
            [Description("This event cannot occur.")]
            Unused_Event_09 = 9,

        //[Category("Debug Events")]
        //[Display(Name = "Debug Mode Activated")]
        //[Description("This event occurs when the debug mode is activated.")]
        //Debug_Mode_Activated = 10,

        //[Category("Debug Events")]
        //[Display(Name = "Debug Mode De-activated")]
        //[Description("This event occurs when the debug mode is de-activated.")]
        //Debug_Mode_Deactivated = 11,

        /*-------------------------------------------------------------------------------------------------------
         * 										   Internal Events
         *-------------------------------------------------------------------------------------------------------*/
        [Category("Internal Events")]
        [Display(Name = "NVRAM - Faulty")]
        [Description("This event occurs when the communication to the NVRAM was tested and failed.")]
        NVRAM_Fail = 10,

        [Category("Internal Events")]
        [Display(Name = "NVRAM - Healthy")]
        [Description("This event occurs when the communication to the NVRAM was tested and passed.")]
        NVRAM_OK = 11,

        [Category("Internal Events")]
        [Display(Name = "Real Time Clock - Faulty")]
        [Description("This event occurs when the communication to the real time clock was tested and failed.")]
        RTC_Fail = 12,

        [Category("Internal Events")]
        [Display(Name = "Real Time Clock - Healthy")]
        [Description("This event occurs when the communication to the real time clock was tested and passed.")]
        RTC_OK = 13,

        [Category("Internal Events")]
        [Display(Name = "System Time - Fail")]
        [Description("This event occurs when the time recieved from the real time clock was tested against a set out criteria to verify the time and it fail.")]
        System_Time_Fail = 14,

        [Category("Internal Events")]
        [Display(Name = "System Time - OK")]
        [Description("This event occurs when the time recieved from the real time clock was tested against a set out criteria to verify the time and it is OK.")]
        System_Time_OK = 15,

        [Category("Internal Events")]
        [Display(Name = "Ambient Light Sensor - Faulty")]
        [Description("This event occurs when the communication to the ambient light sensor was tested and failed.")]
        Ambient_Light_Sensor_Fail = 16,

        [Category("Internal Events")]
        [Display(Name = "Ambient Light Sensor - Healthy")]
        [Description("This event occurs when the communication to the ambient light sensor was tested and passed.")]
        Ambient_Light_Sensor_OK = 17,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 08")]
            [Description("This event cannot occur.")]
            Unused_Event_79 = 18,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 09")]
            [Description("This event cannot occur.")]
            Unused_Event_80 = 19,

        [Category("Internal Events")]
        [Display(Name = "Parameter - Mismatch")]
        [Description("This event occurs when the paramters was checked and failed.")]
        Parameter_Fail = 20,

        [Category("Internal Events")]
        [Display(Name = "Parameter - OK")]
        [Description("This event occurs when the paramters was checked and passed.")]
        Parameter_OK = 21,

        [Category("Internal Events")]
        [Display(Name = "Revision - Mismatch")]
        [Description("This event occurs when the paramter and firmare revisions was compared and is different.")]
        Revision_Fail = 22,

        [Category("Internal Events")]
        [Display(Name = "Revision - OK")]
        [Description("This event occurs when the paramter and firmare revisions was compared and is the same.")]
        Revision_OK = 23,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 10")]
            [Description("This event cannot occur.")]
            Unused_Event_10 = 24,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 11")]
            [Description("This event cannot occur.")]
            Unused_Event_11 = 25,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 12")]
            [Description("This event cannot occur.")]
            Unused_Event_12 = 26,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 13")]
            [Description("This event cannot occur.")]
            Unused_Event_13 = 27,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 14")]
            [Description("This event cannot occur.")]
            Unused_Event_14 = 28,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 15")]
            [Description("This event cannot occur.")]
            Unused_Event_15 = 29,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 1 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 1 communication is tested and failed.")]
        Expansion_Module_1_Fail = 30,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 2 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 2 communication is tested and failed.")]
        Expansion_Module_2_Fail = 31,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 3 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 4 communication is tested and failed.")]
        Expansion_Module_3_Fail = 32,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 4 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 4 communication is tested and failed.")]
        Expansion_Module_4_Fail = 33,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 1 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 1 communication is tested and passed.")]
        Expansion_Module_1_OK = 34,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 2 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 2 communication is tested and passed.")]
        Expansion_Module_2_OK = 35,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 3 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 3 communication is tested and passed.")]
        Expansion_Module_3_OK = 36,

        [Category("Internal Events")]
        [Display(Name = "Expansion Module 4 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 4 communication is tested and passed.")]
        Expansion_Module_4_OK = 37,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 16")]
            [Description("This event cannot occur.")]
            Unused_Event_16 = 38,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 17")]
            [Description("This event cannot occur.")]
            Unused_Event_17 = 39,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 18")]
            [Description("This event cannot occur.")]
            Unused_Event_18 = 40,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 19")]
            [Description("This event cannot occur.")]
            Unused_Event_19 = 41,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 20")]
            [Description("This event cannot occur.")]
            Unused_Event_20 = 42,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 21")]
            [Description("This event cannot occur.")]
            Unused_Event_21 = 43,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 22")]
            [Description("This event cannot occur.")]
            Unused_Event_22 = 44,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 23")]
            [Description("This event cannot occur.")]
            Unused_Event_23 = 45,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 24")]
            [Description("This event cannot occur.")]
            Unused_Event_24 = 46,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 25")]
            [Description("This event cannot occur.")]
            Unused_Event_25 = 47,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 26")]
            [Description("This event cannot occur.")]
            Unused_Event_26 = 48,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 27")]
            [Description("This event cannot occur.")]
            Unused_Event_27 = 49,

        [Category("System Events")]
        [Display(Name = "Expansion Module 1 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 1 antenna's connections is tested and failed.")]
        Expansion_Module_1_Antenna_Fail = 50,

        [Category("System Events")]
        [Display(Name = "Expansion Module 2 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 2 antenna's connections is tested and failed.")]
        Expansion_Module_2_Antenna_Fail = 51,

        [Category("System Events")]
        [Display(Name = "Expansion Module 3 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 3 antenna's connections is tested and failed.")]
        Expansion_Module_3_Antenna_Fail = 52,

        [Category("System Events")]
        [Display(Name = "Expansion Module 4 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 4 antenna's connections is tested and failed.")]
        Expansion_Module_4_Antenna_Fail = 53,

        [Category("System Events")]
        [Display(Name = "Expansion Module 1 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 1 antenna's connections is tested and passed.")]
        Expansion_Module_1_Antenna_OK = 54,

        [Category("System Events")]
        [Display(Name = "Expansion Module 2 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 2 antenna's connections is tested and passed.")]
        Expansion_Module_2_Antenna_OK = 55,

        [Category("System Events")]
        [Display(Name = "Expansion Module 3 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 3 antenna's connections is tested and passed.")]
        Expansion_Module_3_Antenna_OK = 56,

        [Category("System Events")]
        [Display(Name = "Expansion Module 4 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 4 antenna's connections is tested and passed.")]
        Expansion_Module_4_Antenna_OK = 57,

        [Category("System Events")]
        [Display(Name = "Pulse300 RF - Fail")]
        [Description("This event occurs when the Pulse300 device is not detected over RF.")]
        PulseDeviceRF01_Fail = 58,

        [Category("System Events")]
        [Display(Name = "Pulse500 Right RF - Fail")]
        [Description("This event occurs when the right hand side Pulse500 device is not detected over RF.")]
        PulseDeviceRF02_Fail = 59,

        [Category("System Events")]
        [Display(Name = "Pulse500 Rear RF - Fail")]
        [Description("This event occurs when the rear Pulse500 device is not detected over RF.")]
        PulseDeviceRF03_Fail = 60,

        [Category("System Events")]
        [Display(Name = "Pulse500 Left RF - Fail")]
        [Description("This event occurs when the left hand side Puls500 device is not detected over RF.")]
        PulseDeviceRF04_Fail = 61,

        [Category("System Events")]
        [Display(Name = "Pulse500 Front RF - Fail")]
        [Description("This event occurs when the front Pulse500 device is not detected over RF.")]
        PulseDeviceRF05_Fail = 62,

        [Category("System Events")]
        [Display(Name = "Pulse300 RF - OK")]
        [Description("This event occurs when the Pulse300 device is detected over RF.")]
        PulseDeviceRF01_OK = 63,

        [Category("System Events")]
        [Display(Name = "Pulse500 Right RF - OK")]
        [Description("This event occurs when the right hand side Pulse500 device is detected over RF.")]
        PulseDeviceRF02_OK = 64,

        [Category("System Events")]
        [Display(Name = "Pulse500 Rear RF - OK")]
        [Description("This event occurs when the rear Pulse500 device is detected over RF.")]
        PulseDeviceRF03_OK = 65,

        [Category("System Events")]
        [Display(Name = "Pulse500 Left RF - OK")]
        [Description("This event occurs when the left hand side Puls500 device is detected over RF.")]
        PulseDeviceRF04_OK = 66,

        [Category("System Events")]
        [Display(Name = "Pulse500 Front RF - OK")]
        [Description("This event occurs when the front Pulse500 device is detected over RF.")]
        PulseDeviceRF05_OK = 67,

        [Category("System Events")]
        [Display(Name = "Pulse300 CAN - Fail")]
        [Description("This event occurs when the Pulse300 device is not detected over CAN.")]
        PulseDeviceCAN01_Fail = 68,

        [Category("System Events")]
        [Display(Name = "Pulse500 Right CAN - Fail")]
        [Description("This event occurs when the right hand side Pulse500 device is not detected over CAN.")]
        PulseDeviceCAN02_Fail = 69,

        [Category("System Events")]
        [Display(Name = "Pulse500 Rear CAN - Fail")]
        [Description("This event occurs when the rear Pulse500 device is not detected over CAN.")]
        PulseDeviceCAN03_Fail = 70,

        [Category("System Events")]
        [Display(Name = "Pulse500 Left CAN - Fail")]
        [Description("This event occurs when the left hand side Puls500 device is not detected over RF.")]
        PulseDeviceCAN04_Fail = 71,

        [Category("System Events")]
        [Display(Name = "Pulse500 Front CAN - Fail")]
        [Description("This event occurs when the front Pulse500 device is not detected over CAN.")]
        PulseDeviceCAN05_Fail = 72,

        [Category("System Events")]
        [Display(Name = "Pulse300 CAN - OK")]
        [Description("This event occurs when the Pulse300 device is detected over CAN.")]
        PulseDeviceCAN01_OK = 73,

        [Category("System Events")]
        [Display(Name = "Pulse500 Right CAN - OK")]
        [Description("This event occurs when the right hand side Pulse500 device is detected over CAN.")]
        PulseDeviceCAN02_OK = 74,

        [Category("System Events")]
        [Display(Name = "Pulse500 Rear CAN - OK")]
        [Description("This event occurs when the rear Pulse500 device is detected over CAN.")]
        PulseDeviceCAN03_OK = 75,

        [Category("System Events")]
        [Display(Name = "Pulse500 Left CAN - OK")]
        [Description("This event occurs when the left hand side Puls500 device is detected over CAN.")]
        PulseDeviceCAN04_OK = 76,

        [Category("System Events")]
        [Display(Name = "Pulse500 Front CAN - OK")]
        [Description("This event occurs when the front Pulse500 device is detected over CAN.")]
        PulseDeviceCAN05_OK = 77,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 28")]
            [Description("This event cannot occur.")]
            Unused_Event_28 = 78,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 29")]
            [Description("This event cannot occur.")]
            Unused_Event_29 = 79,

        [Category("System Events")]
        [Display(Name = "LightBar - Failure")]
        [Description("This event occurs when the LightBar device is detected over CAN.")]
        Unused_Event_30 = 80,

        [Category("System Events")]
        [Display(Name = "LightBar - OK")]
        [Description("This event occurs when the LightBar device is not detected over CAN.")]
        Unused_Event_31 = 81,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 32")]
            [Description("This event cannot occur.")]
            Unused_Event_32 = 82,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 33")]
            [Description("This event cannot occur.")]
            Unused_Event_33 = 83,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 34")]
            [Description("This event cannot occur.")]
            Unused_Event_34 = 84,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 35")]
            [Description("This event cannot occur.")]
            Unused_Event_35 = 85,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 36")]
            [Description("This event cannot occur.")]
            Unused_Event_36 = 86,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 37")]
            [Description("This event cannot occur.")]
            Unused_Event_37 = 87,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 38")]
            [Description("This event cannot occur.")]
            Unused_Event_38 = 88,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 39")]
            [Description("This event cannot occur.")]
            Unused_Event_39 = 89,

        [Category("System Events")]
        [Display(Name = "Internal Emergency Stop Latch")]
        [Description("This event occurs when the internal emergency stop is pressed and latched.")]
        Estop_Latch_Int = 90,

        [Category("System Events")]
        [Display(Name = "Internal Emergency Stop Release")]
        [Description("This event occurs when the internal emergency stop is released from the latched state.")]
        EStop_Release_Int = 91,

        [Category("System Events")]
        [Display(Name = "External Emergency Stop Latch")]
        [Description("This event occurs when the external emergency stop is pressed and latched.")]
        Estop_Latch_Ext = 92,

        [Category("System Events")]
        [Display(Name = "External Emergency Stop Release")]
        [Description("This event occurs when the external emergency stop is released from the latched state.")]
        Estop_Release_Ext = 93,

        [Category("System Events")]
        [Display(Name = "Input 01 On")]
        [Description("This event occurs when the external input 01 is setected in the on state.")]
        Input_01_On = 94,

        [Category("System Events")]
        [Display(Name = "Input 01 Off")]
        [Description("This event occurs when the external input 01 is setected in the off state.")]
        Input_01_Off = 95,

        [Category("System Events")]
        [Display(Name = "Input 02 On")]
        [Description("This event occurs when the external input 02 is setected in the on state.")]
        Input_02_On = 96,

        [Category("System Events")]
        [Display(Name = "Input 02 Off")]
        [Description("This event occurs when the external input 02 is setected in the off state.")]
        Input_02_Off = 97,

        [Category("System Events")]
        [Display(Name = "Input 03 On")]
        [Description("This event occurs when the external input 03 is setected in the on state.")]
        Input_03_On = 98,

        [Category("System Events")]
        [Display(Name = "Input 03 Off")]
        [Description("This event occurs when the external input 03 is setected in the off state.")]
        Input_03_Off = 99,

        [Category("System Events")]
        [Display(Name = "Input 04 On")]
        [Description("This event occurs when the external input 04 is setected in the on state.")]
        Input_04_On = 100,

        [Category("System Events")]
        [Display(Name = "Input 04 Off")]
        [Description("This event occurs when the external input 04 is setected in the off state.")]
        Input_04_Off = 101,

        [Category("System Events")]
        [Display(Name = "Input 05 On")]
        [Description("This event occurs when the external input 05 is setected in the on state.")]
        Input_05_On = 102,

        [Category("System Events")]
        [Display(Name = "Input 05 Off")]
        [Description("This event occurs when the external input 05 is setected in the off state.")]
        Input_05_Off = 103,

        [Category("System Events")]
        [Display(Name = "Input 06 On")]
        [Description("This event occurs when the external input 06 is setected in the on state.")]
        Input_06_On = 104,

        [Category("System Events")]
        [Display(Name = "Input 06 Off")]
        [Description("This event occurs when the external input 06 is setected in the off state.")]
        Input_06_Off = 105,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 52")]
            [Description("This event cannot occur.")]
            Unused_Event_52 = 106,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 53")]
            [Description("This event cannot occur.")]
            Unused_Event_53 = 107,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 54")]
            [Description("This event cannot occur.")]
            Unused_Event_54 = 108,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 55")]
            [Description("This event cannot occur.")]
            Unused_Event_55 = 109,

        [Category("Operational Events")]
        [Display(Name = "System Pre-op Fail")]
        [Description("This event occurs when the pre-operation test fails.")]
        System_Pre_Op_Pass = 110,

        [Category("Operational Events")]
        [Display(Name = "System Pre-op Pass")]
        [Description("This event occurs when the pre-operation test passed.")]
        System_Pre_Op_Fail = 111,
        
        [Category("Operational Events")]
        [Display(Name = "License Required")]
        [Description("This event occurs when a license is required to operate the unit.")]
        License_Required = 112,

        [Category("Operational Events")]
        [Display(Name = "Admin Required")]
        [Description("This event occurs when a license is required to operate the unit.")]
        Admin_Required = 113,

        [Category("Operational Events")]
        [Display(Name = "License Processed 01")]
        [Description("This event occurs when the license inserted into the unit was processed.")]
        License_Processed_01 = 114,

        [Category("Operational Events")]
        [Display(Name = "License Processed 02")]
        [Description("This event occurs when the license inserted into the unit was processed.")]
        License_Processed_02 = 115,

        [Category("Operational Events")]
        [Display(Name = "License Processed 03")]
        [Description("This event occurs when the license inserted into the unit was processed.")]
        License_Processed_03 = 116,

        [Category("Operational Events")]
        [Display(Name = "License Processed 04")]
        [Description("This event occurs when the license inserted into the unit was processed.")]
        License_Processed_04 = 117,

        [Category("Operational Events")]
        [Display(Name = "License Processed 05")]
        [Description("This event occurs when the license inserted into the unit was processed.")]
        License_Processed_05 = 118,

        [Category("Operational Events")]
        [Display(Name = "License Processed 06")]
        [Description("This event occurs when the license inserted into the unit was processed.")]
        License_Processed_06 = 119,

        [Category("Operational Events")]
        [Display(Name = "License Processing")]
        [Description("This event occurs when the license inserted into the unit was processing.")]
        License_Processing = 120,

        [Category("Operational Events")]
        [Display(Name = "License Invalid")]
        [Description("This event occurs when the license inserted into the unit is not valid.")]
        License_Invalid = 121,

        [Category("Operational Events")]
        [Display(Name = "Licence Warning")]
        [Description("This event occurs when the license inserted into the unit has expired has reached it warning date.")]
        Licence_Warning = 122,

        [Category("Operational Events")]
        [Display(Name = "Licence Expired")]
        [Description("This event occurs when the license inserted into the unit has expired and is no longer valid.")]
        Licence_Expire = 123,

        [Category("Operational Events")]
        [Display(Name = "License Valid")]
        [Description("This event occurs when the license inserted into the unit is valid.")]
        License_Valid = 124,

        [Category("Operational Events")]
        [Display(Name = "Unknown Road Surface")]
        [Description("This event occurs when the road surface is unknown.")]
        UnknownRoad = 125,

        [Category("Operational Events")]
        [Display(Name = "Dry Road Conditons")]
        [Description("This event occurs when dry road conditions is selected.")]
        DryRoad = 126,

        [Category("Operational Events")]
        [Display(Name = "Wet Road Conditions")]
        [Description("This event occurs when wet road conditions is selected.")]
        WetRoad = 127,

        [Category("Operational Events")]
        [Display(Name = "Slippery Road Conditions")]
        [Description("This event occurs when slippery road conditions is selected.")]
        SlipRoad = 128,

        [Category("Operational Events")]
        [Display(Name = "Low Speed Zone")]
        [Description("This event occurs when the system activate low speed zone.")]
        LowSpeedZone = 129,

        [Category("Operational Events")]
        [Display(Name = "Meduim Speed Zone")]
        [Description("This event occurs when the system activate meduim speed zone.")]
        MeduimSpeedZone = 130,

        [Category("Operational Events")]
        [Display(Name = "High Speed Zone")]
        [Description("This event occurs when the system activate high speed zone.")]
        HighSpeedZone = 131,

        [Category("Operational Events")]
        [Display(Name = "Low Speed Zone End")]
        [Description("This event occurs when the system de-activate low speed zone.")]
        LowSpeedZoneEnd = 132,

        [Category("Operational Events")]
        [Display(Name = "Meduim Speed Zone End")]
        [Description("This event occurs when the system de-activate meduim speed zone.")]
        MeduimSpeedZoneEnd = 133,

        [Category("Operational Events")]
        [Display(Name = "High Speed Zone End")]
        [Description("This event occurs when the system de-activate high speed zone.")]
        HighSpeedZoneEnd = 134,

        [Category("Operational Events")]
        [Display(Name = "Speed OK")]
        [Description("This event occurs when the speed of travel is good.")]
        Speed_OK = 135,

        [Category("Operational Events")]
        [Display(Name = "Speed OK End")]
        [Description("This event occurs when the speed of travel exits the OK area.")]
        Speed_OK_End = 136,

        [Category("Operational Events")]
        [Display(Name = "Speed Warning")]
        [Description("This event occurs when the speed of travel is in the warning area.")]
        Speed_Warning = 137,

        [Category("Operational Events")]
        [Display(Name = "Speed Warning End")]
        [Description("This event occurs when the speed of travel exits the the warning area.")]
        Speed_Warning_End = 138,

        [Category("Operational Events")]
        [Display(Name = "Speed Over")]
        [Description("This event occurs when the speed of travel is in the overspeed area.")]
        Speed_Overspeed = 139,

        [Category("Operational Events")]
        [Display(Name = "Speed Over End")]
        [Description("This event occurs when the speed of travel exits the the over speed area.")]
        Speed_Overspeed_End = 140,

        [Category("Operational Events")]
        [Display(Name = "Work Zone Activate 01")]
        [Description("This event occurs when the operator creates a operational work zone.")]
        Operational_Workzone_01 = 141,

        [Category("Operational Events")]
        [Display(Name = "Work Zone Activate 02")]
        [Description("This event occurs when the operator creates a operational work zone.")]
        Operational_Workzone_02 = 142,

        [Category("Operational Events")]
        [Display(Name = "Work Zone End")]
        [Description("This event occurs when the operational de-activate.")]
        Operational_Workzone_End = 143,

        [Category("Operational Events")]
        [Display(Name = "Work Zone Fail")]
        [Description("This event occurs when the operational work zone create failed.")]
        Operational_Workzone_Fail = 144,

        [Category("Operational Events")]
        [Display(Name = "System Action Applied")]
        [Description("This event occurs when the system action that is applied.")]
        SystemAction = 145,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 59")]
            [Description("This event cannot occur.")]
            Unused_Event_59 = 146,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 60")]
            [Description("This event cannot occur.")]
            Unused_Event_60 = 147,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 61")]
            [Description("This event cannot occur.")]
            Unused_Event_61 = 148,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 62")]
            [Description("This event cannot occur.")]
            Unused_Event_62 = 149,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 01")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_01 = 150,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 02")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_02 = 151,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 03")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_03 = 152,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 04")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_04 = 153,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 05")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_05 = 154,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 06")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_06 = 155,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 07")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_07 = 156,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 01 End")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_01_End = 157,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 02 End")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_02_End = 158,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 03 End")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_03_End = 159,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 04 End")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_04_End = 160,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 05 End")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_05_End = 161,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 06 End")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_06_End = 162,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 07 End")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        PDS_07_End = 163,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 63")]
            [Description("This event cannot occur.")]
            Unused_Event_63 = 164,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 64")]
            [Description("This event cannot occur.")]
            Unused_Event_64 = 165,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 65")]
            [Description("This event cannot occur.")]
            Unused_Event_65 = 166,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 66")]
            [Description("This event cannot occur.")]
            Unused_Event_66 = 167,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 67")]
            [Description("This event cannot occur.")]
            Unused_Event_67 = 168,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 68")]
            [Description("This event cannot occur.")]
            Unused_Event_68 = 169,

        [Category("Interface Events")]
        [Display(Name = "J1939  Communication - Failure")]
        [Description("This event occurs when the communication on the CAN Bus fails.")]
        J1939_Failure = 170,

        [Category("Interface Events")]
        [Display(Name = "J1939 Communication - OK")]
        [Description("This event occurs when the communication on the CAN Bus is OK.")]
        J1939_OK = 171,

        [Category("Interface Events")]
        [Display(Name = "J1939 Coolant - Temperature High")]
        [Description("This event occurs when the coolant temperature is higher than the limit.")]
        J1939_Coolant_TempHigh = 172,

        [Category("Interface Events")]
        [Display(Name = "J1939 Coolant - Temperature OK")]
        [Description("This event occurs when the coolant temperature is lower than the limit.")]
        J1939_Coolant_TempOK = 173,

        [Category("Interface Events")]
        [Display(Name = "J1939 Coolant - Pressure High")]
        [Description("This event occurs when the coolant pressure is higher than the limit.")]
        J1939_Coolant_PresHigh = 174,

        [Category("Interface Events")]
        [Display(Name = "J1939 Coolant - Pressure OK")]
        [Description("This event occurs when the coolant pressure is lower than the limit.")]
        J1939_Coolant_PresOK = 175,

        [Category("Interface Events")]
        [Display(Name = "J1939 Oil - Temperature High")]
        [Description("This event occurs when the oil temperature is higher than the limit.")]
        J1939_Oil_TempHigh = 176,

        [Category("Interface Events")]
        [Display(Name = "J1939 Oil - Temperature OK")]
        [Description("This event occurs when the oil temperature is lower than the limit.")]
        J1939_Oil_TempOK = 177,

        [Category("Interface Events")]
        [Display(Name = "J1939 Oil - Pressure High")]
        [Description("This event occurs when the oil pressure is higher than the limit.")]
        J1939_Oil_PresHigh = 178,

        [Category("Interface Events")]
        [Display(Name = "J1939 Oil - Pressure OK")]
        [Description("This occurs when the oil pressure is lower than the limit.")]
        J1939_Oil_PresOK = 179,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 90")]
            [Description("This event cannot occur.")]
            Unused_Event_90 = 180,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 91")]
            [Description("This event cannot occur.")]
            Unused_Event_91 = 181,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 92")]
            [Description("This event cannot occur.")]
            Unused_Event_92 = 182,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 93")]
            [Description("This event cannot occur.")]
            Unused_Event_93 = 183,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 94")]
            [Description("This event cannot occur.")]
            Unused_Event_94 = 184,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 95")]
            [Description("This event cannot occur.")]
            Unused_Event_95 = 185,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 96")]
            [Description("This event cannot occur.")]
            Unused_Event_96 = 186,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 97")]
            [Description("This event cannot occur.")]
            Unused_Event_97 = 187,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 98")]
            [Description("This event cannot occur.")]
            Unused_Event_98 = 188,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 99")]
            [Description("This event cannot occur.")]
            Unused_Event_99 = 189,

        [Category("Interface Events")]
        [Display(Name = "Proximity Detection System - Fault")]
        [Description("This event occurs when a fault is detected on the PDS and sent to the vehicle.")]
        PDS_Fault = 190,

        [Category("Interface Events")]
        [Display(Name = "Proximity Detection System - OK")]
        [Description("This event occurs when no fault is detected on the PDS and sent to the vehicle.")]
        PDS_OK = 191,

        [Category("Interface Events")]
        [Display(Name = "Vehicle - Fault")]
        [Description("This event occurs when a fault is detected on the vehicle and sent to the PDS.")]
        Vehicle_Fault = 192,

        [Category("Interface Events")]
        [Display(Name = "Vehicle - OK")]
        [Description("This event occurs when no fault is detected on the vehicle and sent to the PDS.")]
        Vehicle_OK = 193,

        [Category("Interface Events")]
        [Display(Name = "Emergency Stop Reply")]
        [Description("This event occurs when a message from the OEM is recieved to confirm or ignore the command.")]
        Confirm_Emergency_Stop = 194,

        [Category("Interface Events")]
        [Display(Name = "Controlled Stop Reply")]
        [Description("This event occurs when a message from the OEM is recieved to confirm or ignore the command.")]
        Confirm_Controlled_Stop = 195,

        [Category("Interface Events")]
        [Display(Name = "Slow Down Reply")]
        [Description("This event occurs when a message from the OEM is recieved to confirm or ignore the command.")]
        Confirm_SlowDown = 196,

        [Category("Interface Events")]
        [Display(Name = "Propulsion Bypass Reply")]
        [Description("This event occurs when a message from the OEM is recieved to confirm or ignore the command.")]
        Confirm_Bypass = 197,

        [Category("Interface Events")]
        [Display(Name = "Apply Set Point Reply")]
        [Description("This event occurs when a message from the OEM is recieved to confirm or ignore the command.")]
        Confirm_ApplySetPoint = 198,

        [Category("Interface Events")]
        [Display(Name = "Stand Down Reply")]
        [Description("This event occurs when a message from the OEM is recieved to confirm or ignore the command.")]
        Confirm_StandDown = 199,

        [Category("Interface Events")]
        [Display(Name = "Motion Inhibit Reply")]
        [Description("This event occurs when a message from the OEM is recieved to confirm or ignore the command.")]
        Confirm_Motion_Inhibit = 200,

        [Category("Interface Events")]
        [Display(Name = "Negotiation Incomplete")]
        [Description("This event occurs when the negotiation with the OEM is failed.")]
        Negotiation_Incomplete = 201,

        [Category("Interface Events")]
        [Display(Name = "Negotiation Complete - 01")]
        [Description("This event cannot occur.")]
        Negotiation_Complete_01 = 202,

        [Category("Interface Events")]
        [Display(Name = "Negotiation Complete - 02")]
        [Description("This event cannot occur.")]
        Negotiation_Complete_02 = 203,

        [Category("Interface Events")]
        [Display(Name = "Negotiation Complete - 03")]
        [Description("This event cannot occur.")]
        Negotiation_Complete_03 = 204,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 104")]
            [Description("This event cannot occur.")]
            Unused_Event_103 = 205,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 104")]
            [Description("This event cannot occur.")]
            Unused_Event_104 = 206,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 105")]
            [Description("This event cannot occur.")]
            Unused_Event_105 = 207,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 106")]
            [Description("This event cannot occur.")]
            Unused_Event_106 = 208,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 107")]
            [Description("This event cannot occur.")]
            Unused_Event_107 = 209,

        [Category("Operational Events")]
        [Display(Name = "Advisory Action")]
        [Description("This event display an advisory information event.")]
        AdvisoryAction = 210,

        [Category("Operational Events")]
        [Display(Name = "Advisory Action End")]
        [Description("This event display an advisory end information event.")]
        AdvisoryAction_End = 211,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 110")]
            [Description("This event cannot occur.")]
            Unused_Event_110 = 212,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 111")]
            [Description("This event cannot occur.")]
            Unused_Event_111 = 213,

            [Category("Unused Events")]
            [Display(Name = "Unused Event 112")]
            [Description("This event cannot occur.")]
            Unused_Event_112 = 214,

    }

    public enum PDS_EventTotal
    {
        [Category("Event Total")]
        [Display(Name = "Datalog Entries")]
        [Description("The total of events that were extracted from the datalog.")]
        Total_Events = 0,

        [Category("Event Total")]
        [Display(Name = "Internal Events")]
        [Description("The total of internal events that were extracted from the datalog.")]
        Total_Internal_Events = 1,

        [Category("Event Total")]
        [Display(Name = "System Events")]
        [Description("The total of system events that were extracted from the datalog.")]
        Total_System_Events = 2,

        [Category("Event Total")]
        [Display(Name = "Operational Events")]
        [Description("The total of system events that were extracted from the datalog.")]
        Total_Operational_Events = 3,

        [Category("Event Total")]
        [Display(Name = "Proximity Detection Events")]
        [Description("The total of Proximity Detection events that were extracted from the datalog.")]
        Total_PDS_Events = 4,

        [Category("Event Total")]
        [Display(Name = "Interface Events")]
        [Description("The total of Interface events that were extracted from the datalog.")]
        Total_Interface_Events = 5,

        [Category("Event Total")]
        [Display(Name = "Parameter Change Events")]
        [Description("The total of Parameter Change events that were extracted from the datalog.")]
        Total_ParameterChange_Events = 6,

        [Category("Event Total")]
        [Display(Name = "Discarded Events")]
        [Description("The total of discarded events that were removed from the datalog due to faulty data.")]
        Total_Discarded_Events = 7,

        [Category("Discarded Events")]
        [Display(Name = "Event ID Failure")]
        [Description("The total of discarded events that were removed from the datalog due to faulty data.")]
        Total_EventID_Fail = 8,

        [Category("Discarded Events")]
        [Display(Name = "Invalid Timestamp Failure")]
        [Description("The total of discarded events that were removed from the datalog due to faulty data.")]
        Total_Timestamp_Fail = 9,

        [Category("Discarded Events")]
        [Display(Name = "Default Log Entry")]
        [Description("The total of default entries (0xFF) in the log data.")]
        Total_Default_Time = 10,

        [Category("Discarded Events")]
        [Display(Name = "Empty Log Entry")]
        [Description("The total of empty entries (0x00) in the log data.")]
        Total_Empty_Entries = 11,

        [Category("Filtered Events")]
        [Display(Name = "Filtered Events")]
        [Description("The total of filtered events.")]
        Total_Filtered_Events = 12
    }

    class EventSummaryInformation
    {
        private UInt32 _Event_ID;
        public UInt32 Event_ID
        {
            get { return _Event_ID; }
            set
            {
                if (_Event_ID != value)
                {
                    _Event_ID = value;
                    OnPropertyChanged("Event_ID");
                }
            }
        }

        private UInt32 _Event_Count;
        public UInt32 Event_Count
        {
            get { return _Event_Count; }
            set
            {
                if (_Event_Count != value)
                {
                    _Event_Count = value;
                    OnPropertyChanged("Event_Count");
                }
            }
        }

        private string _Event_Name;
        public string Event_Name
        {
            get { return _Event_Name; }
            set
            {
                if (_Event_Name != value)
                {
                    _Event_Name = value;
                    OnPropertyChanged("Event_Name");
                }
            }
        }

        private string _Event_Group;
        public string Event_Group
        {
            get { return _Event_Group; }
            set
            {
                if (_Event_Group != value)
                {
                    _Event_Group = value;
                    OnPropertyChanged("Event_Group");
                }
            }
        }

        private string _Event_Description;
        public string Event_Description
        {
            get { return _Event_Description; }
            set
            {
                if (_Event_Description != value)
                {
                    _Event_Description = value;
                    OnPropertyChanged("Event_Description");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}