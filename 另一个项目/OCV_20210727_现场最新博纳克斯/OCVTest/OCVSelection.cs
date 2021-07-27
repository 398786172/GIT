namespace OCV
{
    public partial class FrmOCV
    {
        public enum OCVSelection : long
        {
            OCV1_UpperLower_NG=1,//OCV1电池上下限NG
            OCV1_Stdev_NG=2,//OCV1σ判定判定NG

            OCV2_UpperLower_NG =4,//OCV2电池上下限NG
            OCV2_Stdev_NG =8,//OCV2σ判定判定NG

            DeltaV_UpperLower_NG = 16,//ΔV电池上下限NG
            DeltaV_Stdev_NG =32,//ΔVσ判定判定NG
            DeltaV_DeltaV_NG =64,//ΔΔV判定判定NG

            IR_UpperLower_NG =128,//IR1电池上下限NG
            IR_Stdev_NG = 256,//IRσ判定判定NG

            BTNum_NG =512,//电池数量不及格NG

            IR2_UpperLower_NG = 1024,//IR1电池上下限NG
            IR2_Stdev_NG = 2048,//IRσ判定判定NG

            Bat_Not_Exist=4096,//无电池

            //BH20200525增加OCV3-OCV5，IR3-IR5,K2-K4的NG判断
            OCV3_UpperLower_NG = 8192,//OCV3电池上下限NG
            OCV3_Stdev_NG = 16384,//OCV3σ判定判定NG

            OCV4_UpperLower_NG = 32768,//OCV4电池上下限NG
            OCV4_Stdev_NG = 65536,//OCV4σ判定判定NG

            OCV5_UpperLower_NG = 131072,//OCV5电池上下限NG
            OCV5_Stdev_NG = 262144,//OCV5σ判定判定NG

            IR3_UpperLower_NG = 524288,//OCV3电池上下限NG
            IR3_Stdev_NG = 1048576,//OCV3σ判定判定NG

            IR4_UpperLower_NG = 2097152,//OCV4电池上下限NG
            IR4_Stdev_NG = 4194304,//OCV4σ判定判定NG

            IR5_UpperLower_NG = 8388608,//OCV5电池上下限NG
            IR5_Stdev_NG = 16777216,//OCV5σ判定判定NG

            DeltaV2_UpperLower_NG = 33554432,//ΔV电池上下限NG
            DeltaV2_Stdev_NG = 67108864,//ΔVσ判定判定NG
            DeltaV2_DeltaV_NG = 134217728,//ΔΔV判定判定NG

            DeltaV3_UpperLower_NG = 268435456,//ΔV电池上下限NG
            DeltaV3_Stdev_NG = 536870912,//536870912‬,//ΔVσ判定判定NG
            DeltaV3_DeltaV_NG = 1073741824,//ΔΔV判定判定NG

            DeltaV4_UpperLower_NG = 2147483648,//ΔV电池上下限NG
            DeltaV4_Stdev_NG = 4294967296,//ΔVσ判定判定NG
            DeltaV4_DeltaV_NG = 8589934592,//ΔΔV判定判定NG

            OK =0//良品
        }
    }

}