﻿using CefSharp;
using CefSharp.WinForms;
using PokudaSearch.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PokudaSearch.IndexUtil.LuceneIndexBuilder;

namespace PokudaSearch.WebDriver {
    public class WebClawringDriver {

        private const int MaxStackLevel = 5;


        public WebClawringDriver(CefSharpPanel cefSharpPanel) {
            _cefSharpPanel = cefSharpPanel;
        }


        private CefSharpPanel _cefSharpPanel;

        /// <summary>
        /// + 外部ドメインは探索しない。
        /// + 深さ優先で探索し最大N階層までの探索とする。
        /// </summary>
        /// <param name="cefSharpPanel"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, WebContents>> Clawring(string rootUrl) {

            var clawringDic = new Dictionary<string, WebContents>();
            var stackLevel = new Stack<string>();

            string rootAuthority = new Uri(rootUrl).Authority;

            await ClawringRecv(clawringDic, stackLevel, rootUrl, rootAuthority);

            return clawringDic;
        }

        public async Task ClawringRecv(Dictionary<string, WebContents> clawringDic, 
                                         Stack<string> stackLevel, 
                                         string currentUrl,
                                         string rootAuthority) {
            stackLevel.Push(currentUrl);
            var u = new Uri(currentUrl);
            if (u.Authority == rootAuthority &&  //別ドメインへの遷移は無視
                stackLevel.Count <= MaxStackLevel &&  //最大階層を超える場合は無視
                !clawringDic.ContainsKey(currentUrl) && //一度クロールしたページは無視
                currentUrl.IndexOf("#") < 0) { //ページ内リンクは無視

                await _cefSharpPanel.LoadPageAsync(currentUrl);

                AppObject.Logger.Info(stackLevel.Count.ToString() + ":" + currentUrl);

                //攻撃と間違われないように間隔を空けて処理
                Thread.Sleep(100);

                var wc = new WebContents();
                wc.Url = currentUrl;
                wc.Title = await _cefSharpPanel.GetTitle();
                wc.Extention = System.IO.Path.GetExtension(currentUrl);
                wc.UpdateDate = DateTime.Now;
                //UTF8に変換して保持
                byte[] b = Encoding.Default.GetBytes(_cefSharpPanel.CurrentPageSource);
                wc.Contents = Encoding.Unicode.GetString(b);
                wc.Bytes = Encoding.Unicode.GetByteCount(wc.Contents);
                clawringDic.Add(currentUrl, wc);

                var list = await _cefSharpPanel.GetLinks();
                foreach (string url in list) {
                    await ClawringRecv(clawringDic, stackLevel, url, rootAuthority);
                }
            }
            stackLevel.Pop();
        }
    }
}
