namespace OCV
{
    public partial class FrmOCV
    {
        //分选设置类-联动天翼
        class SelectionPara
        {
            public int OCVSelectionWay;//OCV1/2的判定模式，0，上下限判定，1，δ判定

            public double OCVMin;//OCV下限
            
            public double OCVMax;//OCV上限

            public double OCVDelta;//δ最大值

            public double OCVDeltaMin;//δ最小值20200520

            public double OCVavgDeltaMax;//平均值+nδ最小值20200520

            public double OCVavgDeltaMin;//平均值-nδ最小值20200520

            public int OCVn;//n常数值

            public int OCVBatLmt;//OCV判定需要的最小电池数量

            public int InRSelectionWay;//InR1/2的判定模式，0，上下限判定，1，δ判定

            public double InRMin;//InR最小值

            public double InRMax;//InR最大值

            public double InRDelta;//δ最大值

            public double InRDeltaMin;//δ最小值20200520

            public double InRavgDeltaMax;//平均值+nδ最小值20200520

            public double InRavgDeltaMin;//平均值-nδ最小值20200520

            public int InRn;//n常数

            public int InRBatLmt;//InR判定需要的最小电池数量

            public int DeltaVSelectionWay;//ΔV的判断模式

            public double DeltaVMin;//ΔV最小值

            public double DeltaVMax;//ΔV最大值

            public double DeltaVDelta;//ΔV的δ最大值

            public int DeltaVn;//n常数

            public int DeltaDVBatNum;//电池数量最小值

            public int DeltaDVn;//n常数

            public double DeltaDVMin;//Min(ΔΔV-nδ)的最小值

            public double DeltaDVMax;//Min(ΔΔV-nδ)的最大值

            public double DeltaDVMaxMax;//Max(ΔΔV-nδ)的最大值

        }
    }

}