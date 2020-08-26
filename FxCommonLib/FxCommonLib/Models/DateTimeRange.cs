using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Models {

    /// <summary>
    /// 期間クラス
    /// </summary>
    public class DateTimeRange {

        #region Constants

        /// <summary>
        /// 期間重複タイプ
        ///   ・期間1(this)
        ///   ・期間2(parameter)
        ///   の重複関係パターン
        /// </summary>
        public enum IntersectionType {
            /// <summary>
            /// No Intersection
            /// 重複なし
            /// </summary>
            None = -1,
            /// <summary>
            /// Given range ends inside the range
            /// <para>期間2の終了日時が期間1に重複</para>
            /// <para>* 期間1(this)　　　　　　|--------|</para>
            /// <para>* 期間2(parameter)　|--------|</para>
            /// </summary>
            EndsInRange,
            /// <summary>
            /// Given range starts inside the range
            /// <para>期間2の開始日時が期間1に重複</para>
            /// <para>* 期間1(this)　　　　　　|--------|</para>
            /// <para>* 期間2(parameter)　　　　　　|--------| </para>
            /// </summary>
            StartsInRange,
            /// <summary>
            /// Both ranges are equaled
            /// <para>期間2と期間1が同一</para>
            /// <para>* 期間1(this)　　　　　　|--------|</para>
            /// <para>* 期間2(parameter)　　　 |--------| </para>
            /// </summary>
            RangesEqauled,
            /// <summary>
            /// Given range contained in the range
            /// <para>期間2が期間1に内包</para>
            /// <para>* 期間1(this)　　　　　　|--------|</para>
            /// <para>* 期間2(parameter)　　　　 |----| </para>
            /// </summary>
            ContainedInRange,
            /// <summary>
            /// Given range contains the range
            /// <para>期間2が期間1を内包</para>
            /// <para>* 期間1(this)　　　　　　　|----| </para>
            /// <para>* 期間2(parameter)　　　 |--------|</para>
            /// </summary>
            ContainsRange,
        }

        #endregion Constants

        #region Properties
        /// <summary>開始日時</summary>
        private DateTime _Start;

        /// <summary>開始日時</summary>
        public DateTime Start {
            get { return _Start; }
            private set { _Start = value; }
        }

        /// <summary>終了日時</summary>
        private DateTime _End;

        /// <summary>終了日時</summary>
        public DateTime End {
            get { return _End; }
            private set { _End = value; }
        }
        #endregion Properties

        #region Constractors

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <exception cref="System.ArgumentException"></exception>
        public DateTimeRange(DateTime start, DateTime end) {

            if (start > end) {
                throw new System.ArgumentException("Parameters are Illegal, start > end ");
            }

            _Start = start;
            _End = end;
        }
        #endregion Constractors

        #region PublicMethods


        /// <summary>
        /// オブジェクト同士の開始日時と終了日時が同一か否かを比較
        /// </summary>
        /// <param name="range1"></param>
        /// <param name="range2"></param>
        /// <returns></returns>
        public static bool operator ==(DateTimeRange range1, DateTimeRange range2) {
            return range1.Equals(range2);
        }
        /// <summary>
        /// オブジェクト同士の開始日時と終了日時が同一か否かを比較
        /// </summary>
        /// <param name="range1"></param>
        /// <param name="range2"></param>
        /// <returns></returns>
        public static bool operator !=(DateTimeRange range1, DateTimeRange range2) {
            return !(range1 == range2);
        }

        /// <summary>
        /// オブジェクト同士の開始日時と終了日時が同一か否かを比較
        /// </summary>
        /// <param name="range1"></param>
        /// <param name="range2"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj is DateTimeRange) {
                var range1 = this;
                var range2 = (DateTimeRange)obj;
                return range1.Start == range2.Start && range1.End == range2.End;
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        /// <summary>
        /// 期間が重複しているか否かを取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool Intersects(DateTimeRange range) {
            var type = GetIntersectionType(range);
            return type != IntersectionType.None;
        }

        /// <summary>
        /// 日時が期間内か否かを取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsInRange(DateTime date) {
            return (date >= this.Start) && (date <= this.End);
        }

        /// <summary>
        /// 期間の重複タイプを取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public IntersectionType GetIntersectionType(DateTimeRange range) {
            if (this == range) {
                return IntersectionType.RangesEqauled;
            } else if (IsInRange(range.Start) && IsInRange(range.End)) {
                return IntersectionType.ContainedInRange;
            } else if (IsInRange(range.Start)) {
                return IntersectionType.StartsInRange;
            } else if (IsInRange(range.End)) {
                return IntersectionType.EndsInRange;
            } else if (range.IsInRange(this.Start) && range.IsInRange(this.End)) {
                return IntersectionType.ContainsRange;
            }
            return IntersectionType.None;
        }

        /// <summary>
        /// 重複期間を取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public DateTimeRange GetIntersection(DateTimeRange range) {
            var type = this.GetIntersectionType(range);
            if (type == IntersectionType.RangesEqauled || type == IntersectionType.ContainedInRange) {
                return range;
            } else if (type == IntersectionType.StartsInRange) {
                return new DateTimeRange(range.Start, this.End);
            } else if (type == IntersectionType.EndsInRange) {
                return new DateTimeRange(this.Start, range.End);
            } else if (type == IntersectionType.ContainsRange) {
                return this;
            } else {
                return default(DateTimeRange);
            }
        }

        /// <summary>
        /// 重複期間を分数で取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public double GetIntersectionMinites(DateTimeRange range) {

            if (this.Intersects(range)) {
                DateTimeRange dtr = this.GetIntersection(range);

                if (dtr == default(DateTimeRange)) { return 0; }

                return (dtr.End - dtr.Start).TotalMinutes;
            }
            return 0;
        }

        /// <summary>
        /// 期間を文字列で取得
        /// </summary>
        /// <returns>start - end</returns>
        public override string ToString() {
            return Start.ToString() + " - " + End.ToString();
        }

        #endregion PublicMethods
    }

}
