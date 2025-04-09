using System;

namespace Susalem.Common.Triggers
{
    public enum Condition
    {
        AND,
        OR
    }

    public enum Formula
    {
        GreaterThan,
        GreaterThanOrEqual,
        LessThan, 
        LessThanOrEqualOrEqual,
        Equal,
        NotEqual
    }

    public class TriggerRule
    {
        /// <summary>
        /// 位与计算
        /// </summary>
        public Condition Condition { get; set; }

        /// <summary>
        /// 计算方式
        /// </summary>
        public Formula CalFormula { get; set; }

        /// <summary>
        /// 设定值
        /// </summary>
        public double RuleValue { get; set; }

        public string Key { get; set; }

        /// <summary>
        /// 触发规则计算
        /// </summary>
        /// <param name="value">计算值</param>
        /// <param name="previousResult">前一个结果(是否报警)，用作位与计算</param>
        /// <param name="currentResult">当前计算值的结果</param>
        /// <returns>最终结果</returns>
        public bool Execute(double value, bool? previousResult,ref bool currentResult)
        {
            switch (CalFormula)
            {
                case Formula.GreaterThan:
                    currentResult = value > RuleValue;
                    break;
                case Formula.GreaterThanOrEqual:
                    currentResult = value >= RuleValue;
                    break;
                case Formula.LessThan:
                    currentResult = value < RuleValue;
                    break;
                case Formula.LessThanOrEqualOrEqual:
                    currentResult = value <= RuleValue;
                    break;
                case Formula.Equal:
                    currentResult = Math.Abs(value - RuleValue) < 0.001;
                    break;
                case Formula.NotEqual:
                    currentResult = Math.Abs(value - RuleValue) > 0.001;
                    break;
            }

            if (previousResult == null)
            {
                return currentResult;
            }

            switch (Condition)
            {
                case Condition.AND:
                    return currentResult && (bool)previousResult;
                case Condition.OR:
                    return currentResult || (bool)previousResult;
            }

            return currentResult;
        }

        public override string ToString()
        {
            return $"Trigger Rule: {Condition} {Key} {CalFormula} {RuleValue}";
        }
    }
}
