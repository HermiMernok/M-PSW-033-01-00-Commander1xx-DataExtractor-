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
        EV_Empty = 0,

        //Debug event
        [Category("Power Events")]
        [Display(Name = "Power Down")]
        [Description("This event occurs when the unit is powered down.")]
        EV_Power_Down = 1,

        [Category("Power Events")]
        [Display(Name = "Power Up")]
        [Description("This event occurs when the unit is powered up.")]
        EV_Power_Up = 2,

        EV_Card_Inserted = 3,
        EV_PSU = 4,
        EV_SPI = 5,

        //System Errors
        EV_CAN_Fail = 6,
        EV_RF_Fail = 7,
        EV_Admin_Required = 8,
        EV_License_Required = 9,
        EV_License_Processed = 10,
        EV_License_Valid = 11,
        EV_License_Invalid = 12,
        EV_Debug_Mode = 13,
        EV_Pulse400_Fail = 14,
        EV_Antenna_Fail = 15,
        EV_ProximityError = 16,
        EV_Remote_Tilt = 17,
        //Safety Events up to 19

        //Collision Avoidance
        EV_CAS_P_LowSpeed = 23,
        EV_CAS_P_LowSpeed_ACK_Ignored = 24,
        EV_CAS_P_LowSpeed_ACK_Received = 25,
        EV_CAS_W_LowSpeed = 26,
        EV_CAS_W_LowSpeed_ACK_Ignored = 27,
        EV_CAS_W_LowSpeed_ACK_Received = 28,
        EV_CAS_C_LowSpeed = 29,
        EV_CAS_C_LowSpeed_ACK_Ignored = 30,
        EV_CAS_C_LowSpeed_ACK_Received = 31,

        EV_CAS_P_HighSpeed = 32,
        EV_CAS_P_HighSpeed_ACK_Ignored = 33,
        EV_CAS_P_HighSpeed_ACK_Received = 34,
        EV_CAS_W_HighSpeed = 35,
        EV_CAS_W_HighSpeed_ACK_Ignored = 36,
        EV_CAS_W_HighSpeed_ACK_Received = 37,
        EV_CAS_C_HighSpeed = 38,
        EV_CAS_C_HighSpeed_ACK_Ignored = 39,
        EV_CAS_C_HighSpeed_ACK_Received = 40,

        EV_CAS_P_MedSpeed = 41,
        EV_CAS_P_MedSpeed_ACK_Ignored = 42,
        EV_CAS_P_MedSpeed_ACK_Received = 43,
        EV_CAS_W_MedSpeed = 44,
        EV_CAS_W_MedSpeed_ACK_Ignored = 45,
        EV_CAS_W_MedSpeed_ACK_Received = 46,
        EV_CAS_C_MedSpeed = 47,
        EV_CAS_C_MedSpeed_ACK_Ignored = 48,
        EV_CAS_C_MedSpeed_ACK_Received = 49,

        EV_CAS_P_Loco = 50,
        EV_CAS_P_Loco_ACK_Ignored = 51,
        EV_CAS_P_Loco_ACK_Received = 52,
        EV_CAS_W_Loco = 53,
        EV_CAS_W_Loco_ACK_Ignored = 54,
        EV_CAS_W_Loco_ACK_Received = 55,
        EV_CAS_C_Loco = 56,
        EV_CAS_C_Loco_ACK_Ignored = 57,
        EV_CAS_C_Loco_ACK_Received = 58,

        EV_CAS_P_Battery = 59,
        EV_CAS_P_Battery_ACK_Ignored = 60,
        EV_CAS_P_Battery_ACK_Received = 61,
        EV_CAS_W_Battery = 62,
        EV_CAS_W_Battery_ACK_Ignored = 63,
        EV_CAS_W_Battery_ACK_Received = 64,
        EV_CAS_C_Battery = 65,
        EV_CAS_C_Battery_ACK_Ignored = 66,
        EV_CAS_C_Battery_ACK_Received = 67,

        EV_CAS_P_NoGo = 68,
        EV_CAS_P_NoGo_ACK_Ignored = 69,
        EV_CAS_P_NoGo_ACK_Received = 70,
        EV_CAS_W_NoGo = 71,
        EV_CAS_W_NoGo_ACK_Ignored = 72,
        EV_CAS_W_NoGo_ACK_Received = 73,
        EV_CAS_C_NoGo = 74,
        EV_CAS_C_NoGo_ACK_Ignored = 75,
        EV_CAS_C_NoGo_ACK_Received = 76,

        EV_CAS_Person_detected_in_Presence_Zone = 77,
        EV_CAS_Person_detected_in_Presence_Zone_ACK_Ignored = 78,
        EV_CAS_Person_detected_in_Presence_Zone_ACK_Received = 79,
        EV_CAS_Person_detected_in_Warning_Zone = 80,
        EV_CAS_Person_detected_in_Warning_Zone_ACK_Ignored = 81,
        EV_CAS_Person_detected_in_Warning_Zone_ACK_Received = 82,
        EV_CAS_Person_detected_in_Critical_Zone = 83,
        EV_CAS_Person_detected_in_Critical_Zone_ACK_Ignored = 84,
        EV_CAS_Person_detected_in_Critical_Zone_ACK_Received = 85,

        EV_CAS_P_Traffic = 86,
        EV_CAS_P_Traffic_ACK_Ignored = 87,
        EV_CAS_P_Traffic_ACK_Received = 88,
        EV_CAS_W_Traffic = 89,
        EV_CAS_W_Traffic_ACK_Ignored = 90,
        EV_CAS_W_Traffic_ACK_Received = 91,
        EV_CAS_C_Traffic = 92,
        EV_CAS_C_Traffic_ACK_Ignored = 93,
        EV_CAS_C_Traffic_ACK_Received = 94,

        EV_CAS_P_Explosive = 95,
        EV_CAS_P_Explosive_ACK_Ignored = 96,
        EV_CAS_P_Explosive_ACK_Received = 97,
        EV_CAS_W_Explosive = 98,
        EV_CAS_W_Explosive_ACK_Ignored = 99,
        EV_CAS_W_Explosive_ACK_Received = 100,
        EV_CAS_C_Explosive = 101,
        EV_CAS_C_Explosive_ACK_Ignored = 102,
        EV_CAS_C_Explosive_ACK_Received = 103,

        EV_CAS_P_VentDoor = 104,
        EV_CAS_P_VentDoor_ACK_Ignored = 105,
        EV_CAS_P_VentDoor_ACK_Received = 106,
        EV_CAS_W_VentDoor = 107,
        EV_CAS_W_VentDoor_ACK_Ignored = 108,
        EV_CAS_W_VentDoor_ACK_Received = 109,
        EV_CAS_C_VentDoor = 110,
        EV_CAS_C_VentDoor_ACK_Ignored = 111,
        EV_CAS_C_VentDoor_ACK_Received = 112,

        EV_CAS_P_Haulage = 113,
        EV_CAS_P_Haulage_ACK_Ignored = 114,
        EV_CAS_P_Haulage_ACK_Received = 115,
        EV_CAS_W_Haulage = 116,
        EV_CAS_W_Haulage_ACK_Ignored = 117,
        EV_CAS_W_Haulage_ACK_Received = 118,
        EV_CAS_C_Haulage = 119,
        EV_CAS_C_Haulage_ACK_Ignored = 120,
        EV_CAS_C_Haulage_ACK_Received = 121,

        EV_CAS_MenAtWork_detected_in_Presence_Zone = 122,
        EV_CAS_MenAtWork_detected_in_Presence_Zone_ACK_Ignored = 123,
        EV_CAS_MenAtWork_detected_in_Presence_Zone_ACK_Received = 124,
        EV_CAS_MenAtWork_detected_in_Warning_Zone = 125,
        EV_CAS_MenAtWork_detected_in_Warning_Zone_ACK_Ignored = 126,
        EV_CAS_MenAtWork_detected_in_Warning_Zone_ACK_Received = 127,
        EV_CAS_MenAtWork_detected_in_Critical_Zone = 128,
        EV_CAS_MenAtWork_detected_in_Critical_Zone_ACK_Ignored = 129,
        EV_CAS_MenAtWork_detected_in_Critical_Zone_ACK_Received = 130,

        EV_CAS_P_HaulTruckRigid = 131,
        EV_CAS_P_HaulTruckRigid_ACK_Ignored = 132,
        EV_CAS_P_HaulTruckRigid_ACK_Received = 133,
        EV_CAS_HaulTruckRigid_detected_in_Warning_Zone = 134,
        EV_CAS_HaulTruckRigid_detected_in_Warning_Zone_ACK_Ignored = 135,
        EV_CAS_HaulTruckRigid_detected_in_Warning_Zone_ACK_Received = 136,
        EV_CAS_HaulTruckRigid_detected_in_Critical_Zone = 137,
        EV_CAS_HaulTruckRigid_detected_in_Critical_Zone_ACK_Ignored = 138,
        EV_CAS_HaulTruckRigid_detected_in_Critical_Zone_ACK_Received = 139,

        EV_CAS_HaulTruckADT_detected_in_Presence_Zone = 140,
        EV_CAS_HaulTruckADT_detected_in_Presence_Zone_ACK_Ignored = 141,
        EV_CAS_HaulTruckADT_detected_in_Presence_Zone_ACK_Received = 142,
        EV_CAS_HaulTruckADT_detected_in_Warning_Zone = 143,
        EV_CAS_HaulTruckADT_detected_in_Warning_Zone_ACK_Ignored = 144,
        EV_CAS_HaulTruckADT_detected_in_Warning_Zone_ACK_Received = 145,
        EV_CAS_HaulTruckADT_detected_in_Critical_Zone = 146,
        EV_CAS_HaulTruckADT_detected_in_Critical_Zone_ACK_Ignored = 147,
        EV_CAS_HaulTruckADT_detected_in_Critical_Zone_ACK_Received = 148,

        EV_CAS_WaterBowserRigid_detected_in_Presence_Zone = 149,
        EV_CAS_WaterBowserRigid_detected_in_Presence_Zone_ACK_Ignored = 150,
        EV_CAS_WaterBowserRigid_detected_in_Presence_Zone_ACK_Received = 151,
        EV_CAS_WaterBowserRigid_detected_in_Warning_Zone = 152,
        EV_CAS_WaterBowserRigid_detected_in_Warning_Zone_ACK_Ignored = 153,
        EV_CAS_WaterBowserRigid_detected_in_Warning_Zone_ACK_Received = 154,
        EV_CAS_WaterBowserRigid_detected_in_Critical_Zone = 155,
        EV_CAS_WaterBowserRigid_detected_in_Critical_Zone_ACK_Ignored = 156,
        EV_CAS_WaterBowserRigid_detected_in_Critical_Zone_ACK_Received = 157,

        EV_CAS_WaterBowserADT_detected_in_Presence_Zone = 158,
        EV_CAS_WaterBowserADT_detected_in_Presence_Zone_ACK_Ignored = 159,
        EV_CAS_WaterBowserADT_detected_in_Presence_Zone_ACK_Received = 160,
        EV_CAS_WaterBowserADT_detected_in_Warning_Zone = 161,
        EV_CAS_WaterBowserADT_detected_in_Warning_Zone_ACK_Ignored = 162,
        EV_CAS_WaterBowserADT_detected_in_Warning_Zone_ACK_Received = 163,
        EV_CAS_WaterBowserADT_detected_in_Critical_Zone = 164,
        EV_CAS_WaterBowserADT_detected_in_Critical_Zone_ACK_Ignored = 165,
        EV_CAS_WaterBowserADT_detected_in_Critical_Zone_ACK_Received = 166,

        EV_CAS_P_ExcavatorStandard = 167,
        EV_CAS_P_ExcavatorStandard_ACK_Ignored = 168,
        EV_CAS_P_ExcavatorStandard_ACK_Received = 169,
        EV_CAS_W_ExcavatorStandard = 170,
        EV_CAS_W_ExcavatorStandard_ACK_Ignored = 171,
        EV_CAS_W_ExcavatorStandard_ACK_Received = 172,
        EV_CAS_C_ExcavatorStandard = 173,
        EV_CAS_C_ExcavatorStandard_ACK_Ignored = 174,
        EV_CAS_C_ExcavatorStandard_ACK_Received = 175,

        EV_CAS_P_ExcavatorFaceShovel = 176,
        EV_CAS_P_ExcavatorFaceShovel_ACK_Ignored = 177,
        EV_CAS_P_ExcavatorFaceShovel_ACK_Received = 178,
        EV_CAS_W_ExcavatorFaceShovel = 179,
        EV_CAS_W_ExcavatorFaceShovel_ACK_Ignored = 180,
        EV_CAS_W_ExcavatorFaceShovel_ACK_Received = 181,
        EV_CAS_C_ExcavatorFaceShovel = 182,
        EV_CAS_C_ExcavatorFaceShovel_ACK_Ignored = 183,
        EV_CAS_C_EExcavatorFaceShovel_ACK_Received = 184,

        EV_CAS_P_ExcavatorRopeShovel = 185,
        EV_CAS_P_ExcavatorRopeShovel_ACK_Ignored = 186,
        EV_CAS_P_ExcavatorRopeShovel_ACK_Received = 187,
        EV_CAS_W_ExcavatorRopeShovel = 188,
        EV_CAS_W_ExcavatorRopeShovel_ACK_Ignored = 189,
        EV_CAS_W_ExcavatorRopeShovel_ACK_Received = 190,
        EV_CAS_C_ExcavatorRopeShovel = 191,
        EV_CAS_C_ExcavatorRopeShovel_ACK_Ignored = 192,
        EV_CAS_C_ExcavatorRopeShovel_ACK_Received = 193,

        EV_CAS_P_Dragline = 194,
        EV_CAS_P_Dragline_ACK_Ignored = 195,
        EV_CAS_P_Dragline_ACK_Received = 196,
        EV_CAS_W_Dragline = 197,
        EV_CAS_W_Dragline_ACK_Ignored = 198,
        EV_CAS_W_Dragline_ACK_Received = 199,
        EV_CAS_C_Dragline = 200,
        EV_CAS_C_Dragline_ACK_Ignored = 201,
        EV_CAS_C_Dragline_ACK_Received = 202,

        EV_CAS_FrontEndLoader_detected_in_Presence_Zone = 203,
        EV_CAS_FrontEndLoader_detected_in_Presence_Zone_ACK_Ignored = 204,
        EV_CAS_FrontEndLoader_detected_in_Presence_Zone_ACK_Received = 205,
        EV_CAS_FrontEndLoader_detected_in_Warning_Zone = 206,
        EV_CAS_FrontEndLoader_detected_in_Warning_Zone_ACK_Ignored = 207,
        EV_CAS_FrontEndLoader_detected_in_Warning_Zone_ACK_Received = 208,
        EV_CAS_FrontEndLoader_detected_in_Critical_Zone = 209,
        EV_CAS_FrontEndLoader_detected_in_Critical_Zone_ACK_Ignored = 210,
        EV_CAS_FrontEndLoader_detected_in_Critical_Zone_ACK_Received = 211,

        EV_CAS_DozerWheel_detected_in_Presence_Zone = 212,
        EV_CAS_DozerWheel_detected_in_Presence_Zone_ACK_Ignored = 213,
        EV_CAS_DozerWheel_detected_in_Presence_Zone_ACK_Received = 214,
        EV_CAS_DozerWheel_detected_in_Warning_Zone = 215,
        EV_CAS_DozerWheel_detected_in_Warning_Zone_ACK_Ignored = 216,
        EV_CAS_DozerWheel_detected_in_Warning_Zone_ACK_Received = 217,
        EV_CAS_DozerWheel_detected_in_Critical_Zon = 218,
        EV_CAS_DozerWheel_detected_in_Critical_Zon_ACK_Ignored = 219,
        EV_CAS_DozerWheel_detected_in_Critical_Zon_ACK_Received = 220,

        EV_CAS_P_DozerTrack = 221,
        EV_CAS_P_DozerTrack_ACK_Ignored = 222,
        EV_CAS_P_DozerTrack_ACK_Received = 223,
        EV_CAS_W_DozerTrack = 224,
        EV_CAS_W_DozerTrackACK_Ignored = 225,
        EV_CAS_W_DozerTrack_ACK_Received = 226,
        EV_CAS_C_DozerTrack = 227,
        EV_CAS_C_DozerTrack_ACK_Ignored = 228,
        EV_CAS_C_DozerTrack_ACK_Received = 229,

        EV_CAS_P_LDV = 230,
        EV_CAS_P_LDV_ACK_Ignored = 231,
        EV_CAS_P_LDV_ACK_Received = 232,
        EV_CAS_W_LDV = 233,
        EV_CAS_W_LDV_ACK_Ignored = 234,
        EV_CAS_W_LDV_ACK_Received = 235,
        EV_CAS_C_LDV = 236,
        EV_CAS_C_LDV_ACK_Ignored = 237,
        EV_CAS_C_LDV_ACK_Received = 238,

        EV_CAS_P_DrillRig = 239,
        EV_CAS_P_DrillRig_ACK_Ignored = 240,
        EV_CAS_P_DrillRig_ACK_Received = 241,
        EV_CAS_W_DrillRig = 242,
        EV_CAS_W_DrillRig_ACK_Ignored = 243,
        EV_CAS_W_DrillRig_ACK_Received = 244,
        EV_CAS_C_DrillRig = 245,
        EV_CAS_C_DrillRig_ACK_Ignored = 246,
        EV_CAS_C_DrillRig_ACK_Received = 247,

        EV_CAS_P_Grader = 248,
        EV_CAS_P_Grader_ACK_Ignored = 249,
        EV_CAS_P_Grader_ACK_Received = 250,
        EV_CAS_W_Grader = 251,
        EV_CAS_W_Grader_ACK_Ignored = 252,
        EV_CAS_W_Grader_ACK_Received = 253,
        EV_CAS_C_Grader = 254,
        EV_CAS_C_Grader_ACK_Ignored = 255,
        EV_CAS_C_Grader_ACK_Received = 256,

        EV_CAS_P_TLB = 257,
        EV_CAS_P_TLB_ACK_Ignored = 258,
        EV_CAS_P_TLB_ACK_Received = 259,
        EV_CAS_W_TLB = 260,
        EV_CAS_W_TLB_ACK_Ignored = 261,
        EV_CAS_W_TLB_ACK_Received = 262,
        EV_CAS_C_TLB = 263,
        EV_CAS_C_TLB_ACK_Ignored = 264,
        EV_CAS_C_TLB_ACK_Received = 265,

        EV_CAS_P_GoLight = 266,
        EV_CAS_P_GoLightACK_Ignored = 267,
        EV_CAS_P_GoLight_ACK_Received = 268,
        EV_CAS_W_GoLight = 269,
        EV_CAS_W_GoLight_ACK_Ignored = 270,
        EV_CAS_W_GoLight_ACK_Received = 271,
        EV_CAS_C_GoLight = 272,
        EV_CAS_C_GoLightACK_Ignored = 273,
        EV_CAS_C_GoLight_ACK_Received = 274,

        EV_CAS_P_CautionLight = 275,
        EV_CAS_P_CautionLight_ACK_Ignored = 276,
        EV_CAS_P_CautionLight_ACK_Received = 277,
        EV_CAS_W_CautionLight = 278,
        EV_CAS_W_CautionLight_ACK_Ignored = 279,
        EV_CAS_W_CautionLight_ACK_Received = 280,
        EV_CAS_C_CautionLight = 281,
        EV_CAS_C_CautionLight_ACK_Ignored = 282,
        EV_CAS_C_CautionLight_ACK_Received = 283,

        EV_CAS_P_StopLight = 284,
        EV_CAS_P_StopLight_ACK_Ignored = 285,
        EV_CAS_P_StopLight_ACK_Received = 286,
        EV_CAS_W_StopLight = 287,
        EV_CAS_W_StopLight_ACK_Ignored = 288,
        EV_CAS_W_StopLight_ACK_Received = 289,
        EV_CAS_C_StopLight = 290,
        EV_CAS_C_StopLight_ACK_Ignored = 291,
        EV_CAS_C_StopLight_ACK_Received = 292,

        EV_CAS_P_LoadBox = 293,
        EV_CAS_P_LoadBox_ACK_Ignored = 294,
        EV_CAS_P_LoadBox_ACK_Received = 295,
        EV_CAS_W_LoadBox = 296,
        EV_CAS_W_LoadBox_ACK_Ignored = 297,
        EV_CAS_W_LoadBox_ACK_Received = 298,
        EV_CAS_C_LoadBox = 299,
        EV_CAS_C_LoadBox_ACK_Ignored = 300,
        EV_CAS_C_LoadBox_ACK_Received = 301,

        EV_CAS_P_BatteryBay = 302,
        EV_CAS_P_BatteryBay_ACK_Ignored = 303,
        EV_CAS_P_BatteryBay_ACK_Received = 304,
        EV_CAS_W_BatteryBay = 305,
        EV_CAS_W_BatteryBay_ACK_Ignored = 306,
        EV_CAS_W_BatteryBay_ACK_Received = 307,
        EV_CAS_C_BatteryBay = 308,
        EV_CAS_C_BatteryBay_ACK_Ignored = 309,
        EV_CAS_C_BatteryBay_ACK_Received = 310,

        EV_CAS_P_WorkShop = 311,
        EV_CAS_P_WorkShop_ACK_Ignored = 312,
        EV_CAS_P_WorkShop_ACK_Received = 313,
        EV_CAS_W_WorkShop = 314,
        EV_CAS_W_WorkShop_ACK_Ignored = 315,
        EV_CAS_W_WorkShop_ACK_Received = 316,
        EV_CAS_C_WorkShop = 317,
        EV_CAS_C_WorkShop_ACK_Ignored = 318,
        EV_CAS_C_WorkShop_ACK_Received = 319,

        EV_CAS_P_Tips = 320,
        EV_CAS_P_Tips_ACK_Ignored = 321,
        EV_CAS_P_Tips_ACK_Received = 322,
        EV_CAS_W_Tips = 323,
        EV_CAS_W_Tips_ACK_Ignored = 324,
        EV_CAS_W_Tips_ACK_Received = 325,
        EV_CAS_C_Tips = 326,
        EV_CAS_C_Tips_ACK_Ignored = 327,
        EV_CAS_C_Tips_ACK_Received = 328,

        EV_CAS_P_Bend = 329,
        EV_CAS_P_Bend_ACK_Ignored = 330,
        EV_CAS_P_Bend_ACK_Received = 331,
        EV_CAS_W_Bend = 332,
        EV_CAS_W_Bend_ACK_Ignored = 333,
        EV_CAS_W_Bend_ACK_Received = 334,
        EV_CAS_C_Bend = 335,
        EV_CAS_C_Bend_ACK_Ignored = 336,
        EV_CAS_C_Bend_ACK_Received = 337,

        EV_CAS_P_WaitPoint = 338,
        EV_CAS_P_WaitPoint_ACK_Ignored = 339,
        EV_CAS_P_WaitPoint_ACK_Received = 340,
        EV_CAS_W_WaitPoint = 341,
        EV_CAS_W_WaitPoint_ACK_Ignored = 342,
        EV_CAS_W_WaitPoint_ACK_Received = 343,
        EV_CAS_C_WaitPoint = 344,
        EV_CAS_C_WaitPoint_ACK_Ignored = 345,
        EV_CAS_C_WaitPoint_ACK_Received = 346,

        EV_Estop_Latch_Internal = 347,
        EV_System_Idle = 348,
        EV_UBlox_Fail = 349,
        EV_Tag_Test_Passed = 350,


        EV_System_Pre_Operation_Fail = 354,
        EV_System_Pre_Operation_Pass = 355,
        EV_Operational_WorkZone = 357,
        EV_Speedo_Op_Default = 358,
        EV_Speedo_Brake_Test_Static_Pass = 359,
        EV_Speedo_Brake_Test_Static_Fail = 360,
        EV_Speedo_Brake_Test_Active = 361,
        EV_Dynamic_Test_Control = 362,
        EV_Dynamic_Test_Result = 363,
        EV_Wheel_Slip_Active = 364,
        EV_Break_Test_Failed = 365,
        EV_Speedo_Warning = 366,
        EV_Speedo_Overspeeding = 367,
        EV_HUB200_Failure = 368,
        EV_Loco_Motor_Stall = 369,
        EV_Parameter_Mismatch = 370,
        EV_CAS_User_ACK = 371,
        EV_J1939_Failure = 372,
        EV_J1939_Coolant_Error = 373,
        EV_J1939_Oil_Error = 374,
        EV_J1939_Fuel_Error = 375,
        EV_J1939_Rev_Error = 376
    };

    public enum PDS_EventID_V8
    {
        None = 0,
        [Category("Power Events")]
        [Display(Name = "Power Down")]
        [Description("This event occurs when the unit is powered down.")]
        Power_Down = 1,

        [Category("Power Events")]
        [Display(Name = "Power Up")]
        [Description("This event occurs when the unit is powered up.")]
        Power_Up = 2,

        [Category("License Card Events")]
        [Display(Name = "License Card Inserted 00")]
        [Description("This event occurs when a license card is inserted, the information from the license card is logged.")]
        Card_Inserted_00 = 3,

        [Category("License Card Events")]
        [Display(Name = "License Card Inserted 01")]
        [Description("This event occurs when a license card is inserted, the information from the license card is logged.")]
        Card_Inserted_01 = 4,

        [Category("License Card Events")]
        [Display(Name = "License Card Inserted 02")]
        [Description("This event occurs when a license card is inserted, the information from the license card is logged.")]
        Card_Inserted_02 = 5,

        [Category("License Card Events")]
        [Display(Name = "License Card Inserted 03")]
        [Description("This event occurs when a license card is inserted, the information from the license card is logged.")]
        Card_Inserted_03 = 6,

        [Category("License Card Events")]
        [Display(Name = "License Card Inserted 04")]
        [Description("This event occurs when a license card is inserted, the information from the license card is logged.")]
        Card_Inserted_04 = 7,

        [Category("License Card Events")]
        [Display(Name = "License Card Inserted 05")]
        [Description("This event occurs when a license card is inserted, the information from the license card is logged.")]
        Card_Inserted_05 = 8,

        [Category("License Card Events")]
        [Display(Name = "License Card Removed")]
        [Description("This event occurs when a license card is removed.")]
        Card_Removed = 9,

        [Category("Debug Events")]
        [Display(Name = "Debug Mode Activated")]
        [Description("This event occurs when the debug mode is activated.")]
        Debug_Mode_Activated = 10,

        [Category("Debug Events")]
        [Display(Name = "Debug Mode De-activated")]
        [Description("This event occurs when the debug mode is de-activated.")]
        Debug_Mode_Deactivated = 11,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 1 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 1 communication is tested and passed.")]
        Expansion_Module_1_OK = 12,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 1 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 1 communication is tested and failed.")]
        Expansion_Module_1_Fail = 13,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 2 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 2 communication is tested and passed.")]
        Expansion_Module_2_OK = 14,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 2 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 2 communication is tested and failed.")]
        Expansion_Module_2_Fail = 15,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 3 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 3 communication is tested and passed.")]
        Expansion_Module_3_OK = 16,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 4 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 4 communication is tested and failed.")]
        Expansion_Module_3_Fail = 17,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 4 - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 4 communication is tested and passed.")]
        Expansion_Module_4_OK = 18,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 4 - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 4 communication is tested and failed.")]
        Expansion_Module_4_Fail = 19,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 1 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 1 antenna's connections is tested and passed.")]
        Expansion_Module_1_Antenna_OK = 20,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 1 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 1 antenna's connections is tested and failed.")]
        Expansion_Module_1_Antenna_Fail = 21,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 2 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 2 antenna's connections is tested and passed.")]
        Expansion_Module_2_Antenna_OK = 22,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 2 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 2 antenna's connections is tested and failed.")]
        Expansion_Module_2_Antenna_Fail = 23,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 3 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 3 antenna's connections is tested and passed.")]
        Expansion_Module_3_Antenna_OK = 24,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 3 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 3 antenna's connections is tested and failed.")]
        Expansion_Module_3_Antenna_Fail = 25,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 4 Antenna - Healthy")]
        [Description("This event occurs when the module inserted into expansion bay 4 antenna's connections is tested and passed.")]
        Expansion_Module_4_Antenna_OK = 26,

        [Category("System Health Events")]
        [Display(Name = "Expansion Module 4 Antenna - Faulty")]
        [Description("This event occurs when the module inserted into expansion bay 4 antenna's connections is tested and failed.")]
        Expansion_Module_4_Antenna_Fail = 27,

        [Category("System Health Events")]
        [Display(Name = "Power Supply Circuit - Healthy")]
        [Description("This event occurs when power supply circuit is healthy, no fault conditions recieved.")]
        PSU_OK = 28,

        [Category("System Health Events")]
        [Display(Name = "Power Supply Circuit - Faulty")]
        [Description("This event occurs when a fault condition was recieved from the power supply circuit.")]
        PSU_Fail = 29,

        [Category("System Health Events")]
        [Display(Name = "Ambient Light Sensor - Healthy")]
        [Description("This event occurs when the communication to the ambient light sensor was tested and passed.")]
        Ambient_Light_Sensor_OK = 30,

        [Category("System Health Events")]
        [Display(Name = "Ambient Light Sensor - Faulty")]
        [Description("This event occurs when the communication to the ambient light sensor was tested and failed.")]
        Ambient_Light_Sensor_Fail = 31,

        [Category("System Health Events")]
        [Display(Name = "Real Time Clock - Healthy")]
        [Description("This event occurs when the communication to the real time clock was tested and passed.")]
        RTC_OK = 32,

        [Category("System Health Events")]
        [Display(Name = "Real Time Clock - Faulty")]
        [Description("This event occurs when the communication to the real time clock was tested and failed.")]
        RTC_Fail = 33,

        [Category("System Health Events")]
        [Display(Name = "System Time - OK")]
        [Description("This event occurs when the time recieved from the real time clock was tested against a set out criteria to verify the time and it is OK.")]
        System_Time_OK = 34,

        [Category("System Health Events")]
        [Display(Name = "System Time - Fail")]
        [Description("This event occurs when the time recieved from the real time clock was tested against a set out criteria to verify the time and it fail.")]
        System_Time_Fail = 35,

        RF_Own_Tag_OK = 36,
        RF_Own_Tag_Fail = 37,
        CAN_Own_Tag_OK = 38,
        CAN_Own_Tag_Fail = 39,

        [Category("License Card Events")]
        [Display(Name = "License Required")]
        [Description("This event occurs when a license is required to operate the unit.")]
        License_Required = 40,

        [Category("License Card Events")]
        [Display(Name = "License Valid")]
        [Description("This event occurs when the license inserted into the unit is valid.")]
        License_Valid = 41,

        [Category("License Card Events")]
        [Display(Name = "License Invalid")]
        [Description("This event occurs when the license inserted into the unit is not valid.")]
        License_Invalid = 42,

        [Category("License Card Events")]
        [Display(Name = "Licence Warning")]
        [Description("This event occurs when the license inserted into the unit has expired has reached it warning date.")]
        Licence_Warning = 43,

        [Category("License Card Events")]
        [Display(Name = "Licence Expired")]
        [Description("This event occurs when the license inserted into the unit has expired and is no longer valid.")]
        Licence_Expire = 44,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 00")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        EV_CAS_Threat_00 = 45,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 01")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        EV_CAS_Threat_01 = 46,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 02")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        EV_CAS_Threat_02 = 47,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 03")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        EV_CAS_Threat_03 = 48,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 04")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        EV_CAS_Threat_04 = 49,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 05")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        EV_CAS_Threat_05 = 50,

        [Category("Proximity Detection Events")]
        [Display(Name = "Proximity Detection Event - 06")]
        [Description("This event occurs when a threat is detected and the information of the possible collision is logged.")]
        EV_CAS_Threat_06 = 51,
        //EV_Operational_WorkZone_00
        //EV_Operational_WorkZone_01
        //EV_Operational_WorkZone_Fail
        //EV_Operational_WorkZone_End
        //EV_User_ACK_CAS
        //EV_Estop_Internal_Latch
        //EV_Estop_Internal_Release
        //EV_Estop_External_Latch
        //EV_Estop_External_Release
        //EV_Idle
        //EV_Action_1
        //EV_Action_2
        //EV_Action_3
        //EV_Action_Return
        //EV_Tag_Test_Passed
        //EV_Tag_Test_Failed
        //EV_Tag_Test_TimeOut
        //EV_ProximityError
        //System_Pre_Op_Fail
        //System_Pre_Op_Pass
        //System_Pre_Op_Bypassed
        //EV_Speedo_Op_Default
        //RBE_Brake_Test_Static_Pass
        //RBE_Brake_Test_Static_Fail
        //RBE_Brake_Test_Active
        //RBE_Dynamic_Test_Control
        //RBE_Dynamic_Test_Result
        //RBE_Wheel_Slip_Active
        //RBE_Break_Test_Pass
        //RBE_Break_Test_Failed
        //RBE_Motor_Stall
        //Ground_Speed_OK
        //Ground_Speed_OK_End
        //Ground_Speed_Warning
        //Ground_Speed_Warning_End
        //Ground_Speed_Overspeed
        //Ground_Speed_Overspeed_End
        //Parameter_Mismatch
        //Commissioning_Mode
        //HUB200_OK
        //HUB200_Failure
        //FLOW_OK
        //FLOW_Fail
        //LightBar_OK
        //LightBar_Fail
        //Remote_OK
        //Remote_Fail
        //GUI_Communication_Active
        //GUI_Communication_Lost
        //Output_01_On
        //Output_01_Off
        //Output_02_On
        //Output_02_Off
        //Output_03_On
        //Output_03_Off
        //Input_01_On
        //Input_01_Off
        //Input_02_On
        //Input_02_Off
        //Input_03_On
        //Input_03_Off
        //Input_04_On
        //Input_04_Off
        //Input_05_On
        //Input_05_Off
        //Input_06_On
        //Input_06_Off
        //EV_J1939_Failure
        //EV_J1939_Coolant_Error
        //EV_J1939_Oil_Error
        //EV_J1939_Fuel_Error
        //EV_J1939_Rev_Error
        //PDS_Wet_Road_Conditions
        //PDS_Dry_Road_Conditions
        //PDS_Status_OK
        //PDS_Status_Fail
        //PDS_Emergency_Stop
        //PDS_Controlled_Stop
        //PDS_SlowDown
        //PDS_ByPass_Propulsion
        //PDS_Motion_Inhibit
        //PDS_StandDown
        //Machine_Status_OK
        //Machine_Payload_Loaded
        //Machine_Confirm_Emergency_Stop
        //Machine_Confirm_Controlled_Stop
        //Machine_Confirm_SlowDown
        //Machine_Confirm_ByPass_Propulsion
        //Machine_Confirm_Motion_Inhibit
        //Machine_Confirm_StandDown
        //Machine_Status_Fail
        //Machine_Payload_Empty
        //Machine_Ignore_Emergency_Stop
        //Machine_Ignore_Controlled_Stop
        //Machine_Ignore_SlowDown
        //Machine_Ignore_ByPass_Propulsion
        //Machine_Ignore_Motion_Inhibit
        //Machine_Ignore_StandDown
        //Machine_Override_PDS
        //Machine_Traction_Control
        //Machine_RollBack

    }
    public enum PDS_EventTotal
    {
        [Category("Event Total")]
        [Display(Name = "Total Events")]
        [Description("The total of events that were extracted from the datalog")]
        Total_Events = 0,

        [Category("Event Total")]
        [Display(Name = "Total Proximity Detection Events")]
        [Description("The total of Proximity Detection events that were extracted from the datalog")]
        Total_PDS_Events = 1,

        [Category("Event Total")]
        [Display(Name = "Total System Failure Events")]
        [Description("The total of System Failure events that were extracted from the datalog")]
        Total_SystemFail_Events = 2,

        [Category("Event Total")]
        [Display(Name = "Total Parameter Change Events")]
        [Description("The total of Parameter Change events that were extracted from the datalog")]
        Total_ParameterChange_Events = 3,

        [Category("Event Total")]
        [Display(Name = "Total Discarded Events")]
        [Description("The total of discarded events that were removed from the datalog due to faulty data")]
        Total_Discarded_Events = 4,

        [Category("Discarded Events")]
        [Display(Name = "Total Event ID Failure")]
        [Description("The total of discarded events that were removed from the datalog due to faulty data")]
        Total_EventID_Fail = 5,

        [Category("Discarded Events")]
        [Display(Name = "Total Invalid Timestamp Events")]
        [Description("The total of discarded events that were removed from the datalog due to faulty data")]
        Total_Timestamp_Fail = 6,

        [Category("Filtered Events")]
        [Display(Name = "Total Filtered Events")]
        [Description("The total of filtered events")]
        Total_Filtered_Events = 7
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