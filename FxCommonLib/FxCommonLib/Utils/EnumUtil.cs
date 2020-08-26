using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace FxCommonLib.Utils {
    /// <summary>
    /// 列挙型のフィールドにラベル文字列を付加するカスタム属性。
    /// </summary>
    /// <seealso cref="https://irislab.input/blog/20151124/enumext/"/>
    //HACK 使い方のサンプルが必要
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumLabel : Attribute {
        public EnumLabel(string label) {
            Label = label;
        }
        public string Label { get; set; }
    }

    /// <summary>
    /// Enumユーティリティクラス
    /// </summary>
    public static class EnumUtil {
        private static Dictionary<Enum, string> _textCache = new Dictionary<Enum, string>();

        /// <summary>
        /// Enumの項目名を取得
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetName(Enum e) {
            return Enum.GetName(e.GetType(), e);
        }

        /// <summary>
        /// Enumの項目数を取得
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int GetCount(Type t) {
            return Enum.GetNames(t).Length;
        }

        /// <summary>
        /// ラベル文字列を取得
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>ラベル文字列</returns>
        public static string GetLabel(this Enum instance) {
            lock (_textCache) {
                if (_textCache.ContainsKey(instance)) {
                    return _textCache[instance];
                }

                var instanceType = instance.GetType();

                Func<Enum, string> enumToText = delegate(Enum enumElement) {
                    if (_textCache.ContainsKey(enumElement)) {
                        return _textCache[enumElement];
                    }

                    var attributes
                        = instanceType.GetField(enumElement.ToString()).GetCustomAttributes(typeof(EnumLabel), true);
                    if (attributes.Length == 0) {
                        return instance.ToString();
                    }

                    var enumText = ((EnumLabel)attributes[0]).Label;
                    _textCache.Add(enumElement, enumText);

                    return enumText;
                };

                if (Enum.IsDefined(instanceType, instance)) {
                    return enumToText(instance);
                } else if (instanceType.GetCustomAttributes(typeof(System.FlagsAttribute), true).Length > 0) {
                    var instanceValue = Convert.ToInt64(instance);

                    var enumes =
                        from Enum value in Enum.GetValues(instanceType)
                        where (instanceValue & Convert.ToInt64(value)) != 0
                        select value;

                    var enumSumValue =
                        enumes.Sum(value => Convert.ToInt64(value));

                    if (enumSumValue != instanceValue) return instance.ToString();

                    var enumText = string.Join(", ",
                        (from Enum value in enumes
                         select enumToText(value)).ToArray());

                    if (!_textCache.ContainsKey(instance)) {
                        _textCache.Add(instance, enumText);
                    }

                    return enumText;
                } else {
                    return instance.ToString();
                }
            }
        }
    }
}
