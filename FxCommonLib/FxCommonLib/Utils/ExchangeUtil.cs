using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System;

namespace FxCommonLib.Utils {
    /// <summary>
    /// 為替ユーティリティ
    /// NOTE: Yahoo Finance廃止
    /// NOTE: Google Finance廃止
    /// NOTE: 外為オンラインから取得
    /// </summary>
    public class ExchangeUtil : PublicWebApiUtil {

        #region Consts
        public enum CurrencyPairCode : int {
            /// <summary>英ポンド/NZドル</summary>
            GBPNZD = 1,
            /// <summary>カナダドル/NZドル</summary>
            CADJPY,
            /// <summary>英ポンド/AUドル</summary>
            GBPAUD,
            /// <summary>AUドル/円</summary>
            AUDJPY,
            /// <summary>AUドル/NZドル</summary>
            AUDNZD,
            /// <summary>ユーロ/カナダドル</summary>
            EURCAD,
            /// <summary>ユーロ/米ドル</summary>
            EURUSD,
            /// <summary>NZドル/円</</summary>
            NZDJPY,
            /// <summary>米ドル/カナダドル</</summary>
            USDCAD,
            /// <summary>ユーロ/英ポンド</</summary>
            EURGBP,
            /// <summary>英ポンド/米ドル</</summary>
            GBPUSD,
            /// <summary>南アフリカランド/円</</summary>
            ZARJPY,
            /// <summary>ユーロ/スイスフラン</</summary>
            EURCHF,
            /// <summary>スイスフラン/円</</summary>
            CHFJPY,
            /// <summary>AUドル/米ドル</</summary>
            AUDUSD,
            /// <summary>AUドル/米ドル</</summary>
            USDCHF,
            /// <summary>ユーロ/円</</summary>
            EURJPY,
            /// <summary>英ポンド/スイスフラン</</summary>
            GBPCHF,
            /// <summary>ユーロ/NZドル</</summary>
            EURNZD,
            /// <summary>NZドル/円</</summary>
            NZDUSD,
            /// <summary>米ドル/円</</summary>
            USDJPY,
            /// <summary>ユーロ/AUドル</</summary>
            EURAUD,
            /// <summary>AUドル/スイスフラン</</summary>
            AUDCHF,
            /// <summary>英ポンド/円</summary>
            GBPJPY
        }
        public enum RateTableColIdx : int {
            /// <summary>高値</summary>
            [EnumLabel("high")]
            High = 0,
            /// <summary>始値</summary>
            [EnumLabel("open")]
            Open,
            /// <summary>売値</summary>
            [EnumLabel("bid")]
            Bid,
            /// <summary>貨幣コード</summary>
            [EnumLabel("currencyPairCode")]
            CurrencyPairCode,
            /// <summary>買値</summary>
            [EnumLabel("ask")]
            Ask,
            /// <summary>安値</summary>
            [EnumLabel("low")]
            Low
        }
        #endregion Consts

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="smtpAddress"></param>
        /// <param name="smtpPort"></param>
        public ExchangeUtil() {
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// 為替情報レートテーブルを取得
        /// </summary>
        /// <returns></returns>
        public DataTable GetExchangeRateTable() {
            string url = "https://www.gaitameonline.com/rateaj/getrate";

            string jsonString = GetResponseString(url); 
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(jsonString);

            return ds.Tables[0];
        }

        /// <summary>
        /// 指定した貨幣コードの始値を取得
        /// </summary>
        /// <param name="currencyPairCode"></param>
        /// <returns></returns>
        public Decimal GetOpenPrice(CurrencyPairCode currencyPairCode) {
            Decimal price = -1;

            DataTable exchangeTbl = GetExchangeRateTable();
            DataRow[] rows = (
                from row in exchangeTbl.AsEnumerable()
                let qOpenPrice = row.Field<string>(EnumUtil.GetLabel(RateTableColIdx.Open))
                let qCurrencyPairCode = row.Field<string>(EnumUtil.GetLabel(RateTableColIdx.CurrencyPairCode))
                where qCurrencyPairCode == EnumUtil.GetName(currencyPairCode)
                select row).ToArray();

            price = Decimal.Parse(rows[0][(int)RateTableColIdx.Open].ToString());

            return price;
        }
        #endregion PublicMethods
    }
}
