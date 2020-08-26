using FxCommonLib.Utils;
using System.Text.RegularExpressions;

namespace FxCommonLib.Consts.MES {
    /// <summary>
    /// 共通定数クラス
    /// </summary>
    public static class MESConsts {

        #region Enum
        /// <summary>パネルID</summary>
        public enum Panel : int {
            ImpendingListPanel = 1, //納期逼迫リスト
            FailureReportPanel = 2, //不良速報リスト
            OperationMonitorPanel = 3, //部署別稼働率
            ItemFailureRatePanel = 4, //品目別不良率
            ParetoFailureContentPanel = 5, //不良内容パレート図
            ItemDeliveryRatePanel = 6, //品目別納期遵守率
            ActualLeadTimePanel = 7, //品目別平均実績時間 //HACK ActualAvgTimePanelに変更すべき？
            ParetoFailureCausePanel = 8, //不良原因パレート図
            IndirectOpeTimePanel = 9, //間接作業時間
            //NOTE:カスタマイズパネルなので取り敢えず三桁にした
            EachResponsiblePanel = 101, //各担当パネル
            InSitePlanPanel = 102, //場内計画パネル
            PlanBeforeProcPanel = 103, //加工前準備計画
        }
        /// <summary>備考編集タイプ</summary>
        public enum NoteEditType : int {
            NCPgNo = 1,
            ItemNote1,
            ItemNote2,
            ItemNote3,
            ItemNote4,
            ItemNote5
        }
        /// <summary>面取り加工タイプ</summary>
        public enum ChamferType : int {
            None = 0,                    //0：なし
            TwoFWidth = 1,               //1：2F[巾のみ]
            FourFThicknessWidth = 2,     //2:4F[厚、巾]
            FourFWidthLength = 3,        //3:4F[巾、長]
            SixFThicknessWidthLength = 4 //4:6F[厚巾長]
        }

        /// <summary>入出庫区分モード</summary>
        public enum InOutDivMode : int {
            [EnumLabel("I")]
            PlannedIn = 0,      //計画入庫
            [EnumLabel("O")]
            PlannedOut,         //計画出庫
            [EnumLabel("I")]
            UnplannedIn,        //計画外入庫
            [EnumLabel("O")]
            UnplannedOut,       //計画外出庫
            [EnumLabel("D")]
            Discard,            //廃棄
            [EnumLabel("M")]
            Move                //移動
        }

        /// <summary>入出庫即時実行タイプ</summary>
        public enum InventoryPromptType : int {
            Noop = 0,   //実行しない
            Exec,       //実行する
            Already,    //既に存在する
        }

        /// <summary>棚卸更新区分</summary>
        public enum PhisicalInventoryDiv {
            Begin = 0,
            Halfway,
            End,
        }

        /// <summary>現品票帳票タイプ</summary> // NOTE:SysCodeの設定内容と同一とする必要あり
        public enum IdentificationReportType : int {
            /// <summary>現品票</summary>
            [EnumLabel("IdentificationReport")]
            IdentificationReport = 0
        }

        /// <summary>在庫帳票タイプ</summary> // NOTE:SysCodeの設定内容と同一とする必要あり
        public enum InventoryReportType : int {
            [EnumLabel("InventoryReport")]
            InventoryReport = 0
        }

        /// <summary>社内注文書帳票タイプ</summary> // NOTE:現時点でNKT専用となっている
        public enum SalesOrderReportType : int {
            /// <summary>社内注文書</summary>
            [EnumLabel("SalesOrderReport")]
            SalesOrderReport = 0,
            /// <summary>材料注文書</summary>
            [EnumLabel("MaterialOrderReport")]
            MaterialOrderReport,
            /// <summary>ヘリサート必要予定表</summary>
            [EnumLabel("HelicoidScheduleReport")]
            HelicoidScheduleReport,
            /// <summary>表面処理注文書</summary>
            [EnumLabel("SurfaceOrderReport")]
            SurfaceOrderReport
        }

        /// <summary>NTN工程管理票帳票タイプ</summary> // NOTE:SysCodeの設定内容と同一とする必要あり
        public enum NTNProcReportType : int {
            /// <summary>場外工程管理票</summary>
            [EnumLabel("JyogaiProcReport")]
            JyogaiProcReport = 0,
            /// <summary>場内工程管理票</summary>
            [EnumLabel("JyonaiProcReport")]
            JyonaiProcReport,
            /// <summary>組立工程管理票</summary>
            [EnumLabel("KumitateProcReport")]
            KumitateProcReport
        }

        /// <summary>NTN中期計画表帳票タイプ</summary> // NOTE:SysCodeの設定内容と同一とする必要あり
        public enum NTNMidReportType : int {
            /// <summary>アクスル中期計画(試作品集約No単位)</summary>
            [EnumLabel("AxleMidAggregationReport")]
            AxleMidAggregationReport = 0,
            /// <summary>テーパー中期計画(試作品集約No単位)</summary>
            [EnumLabel("TaperMidAggregationReport")]
            TaperMidAggregationReport,
            /// <summary>アクスル中期計画(試作品登録No単位)</summary>
            [EnumLabel("AxleMidReport")]
            AxleMidReport,
            /// <summary>テーパー中期計画(試作品登録No単位)</summary>
            [EnumLabel("TaperMidReport")]
            TaperMidReport
        }

        /// <summary>NTN現品票／材入票タイプ</summary> // NOTE:SysCodeの設定内容と同一とする必要あり
        public enum NTNTagReportType : int {
            /// <summary>現品票</summary>
            [EnumLabel("TagIdentificationReport")]
            TagIdentificationReport = 0,
            /// <summary>材入票</summary>
            [EnumLabel("TagMaterialReport")]
            TagMaterialReport
        }


        #endregion Enum

        /// <summary>削除グループのコード</summary>
        public const string DeleteGroupCode = "#DeleteGroup";

        /// <summary>データディクショナリ</summary>
        #region DataDictionary
        /// <summary>作業開始前</summary>
        public const string pre_start = "pre_start";
        /// <summary>グループ所属</summary>
        public const string is_grouped = "is_grouped";
        /// <summary>グループID</summary>
        public const string group_id = "group_id";
        /// <summary>グループ名</summary>
        public const string group_name = "group_name";
        /// <summary>グループステータス</summary>
        public const string group_status = "group_status";
        /// <summary>グループステータス名</summary>
        public const string group_status_name = "group_status_name";
        /// <summary>工具情報</summary>
        public const string tool_info = "tool_info";
        /// <summary>不良情報</summary>
        public const string failure_info = "failure_info";
        /// <summary>計画メモ情報</summary>
        public const string plan_memo_info = "plan_memo_info";
        /// <summary>実績メモ情報</summary>
        public const string result_memo_info = "result_memo_info";
        /// <summary>承認</summary>
        public const string approval = "approval";
        /// <summary>承認更新日時</summary>
        public const string approval_date = "approval_date";
        /// <summary>承認更新者</summary>
        public const string approval_name = "approval_name";
        /// <summary>認証文字列</summary>
        public const string auth_str = "auth_str";
        /// <summary>権限CD</summary>
        public const string authority_cd = "authority_cd";
        /// <summary>クライアントID</summary>
        public const string client_id = "client_id";
        /// <summary>クライアント名</summary>
        public const string client_name = "client_name";
        /// <summary>クライアント備考1</summary>
        public const string client_note1 = "client_note1";
        /// <summary>クライアント備考2</summary>
        public const string client_note2 = "client_note2";
        /// <summary>クライアント備考3</summary>
        public const string client_note3 = "client_note3";
        /// <summary>クライアント備考4</summary>
        public const string client_note4 = "client_note4";
        /// <summary>クライアント備考5</summary>
        public const string client_note5 = "client_note5";
        /// <summary>分割作業コード拡張部</summary>
        public const string code_ex = "code_ex";
        /// <summary>コメント</summary>
        public const string comments = "comments";
        /// <summary>得意先名</summary>
        public const string customer_name = "customer_name";
        /// <summary>納期</summary>
        public const string deadline = "deadline";
        /// <summary>予定日時指定フラグ</summary>
        public const string estima_times = "estima_times";
        /// <summary>不良数量</summary>
        public const string failure_qty = "failure_qty";
        /// <summary>ファイル名</summary>
        public const string file_name = "file_name";
        /// <summary>フラグ</summary>
        public const string flags = "flags";
        /// <summary>未完了数量</summary>
        public const string incomplete_qty = "incomplete_qty";
        /// <summary>モバイル版利用権限</summary>
        public const string is_mobile = "is_mobile";
        /// <summary>アップロードフラグ</summary>
        public const string is_update = "is_update";
        /// <summary>商品コード</summary>
        public const string item = "item";
        /// <summary>最終ログイン日時</summary>
        public const string last_login_date = "last_login_date";
        /// <summary>製造終了日時(計画)</summary>
        public const string plan_manu_end = "plan_manu_end";
        /// <summary>製造開始日時(計画)</summary>
        public const string plan_manu_start = "plan_manu_start";
        /// <summary>製造時間(計画)</summary>
        public const string plan_manu_working = "plan_manu_working";
        /// <summary>製造時間(計画)h</summary>
        public const string plan_manu_working_h = "plan_manu_working_h";
        /// <summary>メモ</summary>
        public const string memo = "memo";
        /// <summary>割付け方法コード</summary>
        public const string method = "method";
        /// <summary>割付け方法</summary>
        public const string method_name = "method_name";
        /// <summary>数値仕様</summary>
        public const string numspecs = "numspecs";
        /// <summary>作業終了日時(計画)</summary>
        public const string plan_ope_end = "plan_ope_end";
        /// <summary>作業開始日時(計画)</summary>
        public const string plan_ope_start = "plan_ope_start";
        /// <summary>作業時間(計画)</summary>
        public const string plan_ope_working = "plan_ope_working";
        /// <summary>作業時間(計画)h</summary>
        public const string plan_ope_working_h = "plan_ope_working_h";
        /// <summary>作業コード</summary>
        public const string operation_cd = "operation_cd";
        /// <summary>オーダーコード</summary>
        public const string order_cd = "order_cd";
        /// <summary>オーダー名</summary>
        public const string order_exact_name = "order_exact_name";
        /// <summary>オーダー猶予日数</summary>
        public const string order_postponement = "order_postponement";
        /// <summary>オーダー計画終了日時</summary>
        public const string order_ppt = "order_ppt";
        /// <summary>オーダー優先度</summary>
        public const string order_priority = "order_priority";
        /// <summary>オーダー数量</summary>
        public const string order_qty = "order_qty";
        /// <summary>オーダー作業状況コード</summary>
        public const string order_work_status_cd = "order_work_status_cd";
        /// <summary>オーダー作業期間終了日時</summary>
        public const string order_wpet = "order_wpet";
        /// <summary>オーダー作業期間開始日時</summary>
        public const string order_wpst = "order_wpst";
        /// <summary>数量</summary>
        public const string out_qty = "out_qty";
        /// <summary>パスワード</summary>
        public const string password = "password";
        /// <summary>新パスワード</summary>
        public const string new_password = "new_password";
        /// <summary>計画終了日時</summary>
        public const string ppt = "ppt";
        /// <summary>主資源</summary>
        public const string primary_resource = "primary_resource";
        /// <summary>作業着手状況</summary>
        public const string progress_status = "progress_status";
        /// <summary>完了実績上流伝播</summary>
        public const string propagate = "propagate";
        /// <summary>見込みレベルコード</summary>
        public const string prospect = "prospect";
        /// <summary>パスワード変更日時</summary>
        public const string pwd_up_date = "pwd_up_date";
        /// <summary>パスワード変更者ID</summary>
        public const string pwd_up_id = "pwd_up_id";
        /// <summary>パスワード変更者名</summary>
        public const string pwd_up_name = "pwd_up_name";
        /// <summary>実績収集日時</summary>
        public const string result_collection = "result_collection";
        /// <summary>実績登録日</summary>
        public const string result_insert_date = "result_insert_date";
        /// <summary>実績登録者</summary>
        public const string result_insert_name = "result_insert_name";
        /// <summary>製造終了日時(実績)</summary>
        public const string result_manu_end = "result_manu_end";
        /// <summary>製造時間(実績)</summary>
        public const string result_manu_minutes = "result_manu_minutes";
        /// <summary>製造時間(実績)h</summary>
        public const string result_manu_hours = "result_manu_hours";
        /// <summary>製造開始日時(実績)</summary>
        public const string result_manu_start = "result_manu_start";
        /// <summary>実績数量</summary>
        public const string result_qty = "result_qty";
        /// <summary>実績進捗率</summary>
        public const string result_rate = "result_rate";
        /// <summary>実績利用資源</summary>
        public const string result_resources = "result_resources";
        /// <summary>前段取り終了日時(実績)</summary>
        public const string result_setup_end = "result_setup_end";
        /// <summary>前段取り時間(実績)</summary>
        public const string result_setup_minutes = "result_setup_minutes";
        /// <summary>前段取り時間(実績)h</summary>
        public const string result_setup_hours = "result_setup_hours";
        /// <summary>前段取り開始日時(実績)</summary>
        public const string result_setup_start = "result_setup_start";
        /// <summary>作業実績ステータスコード</summary>
        public const string result_status_cd = "result_status_cd";
        /// <summary>作業実績ステータス</summary>
        public const string result_status = "result_status";
        /// <summary>後段取り終了日時(実績)</summary>
        public const string result_teardown_end = "result_teardown_end";
        /// <summary>後段取り時間(実績)</summary>
        public const string result_teardown_minutes = "result_teardown_minutes";
        /// <summary>後段取り時間(実績)h</summary>
        public const string result_teardown_hours = "result_teardown_hours";
        /// <summary>後段取り開始日時(実績)</summary>
        public const string result_teardown_start = "result_teardown_start";
        /// <summary>実績種別</summary>
        public const string result_type = "result_type";
        /// <summary>実績更新日</summary>
        public const string result_update_date = "result_update_date";
        /// <summary>実績更新者</summary>
        public const string result_update_name = "result_update_name";
        /// <summary>確認</summary>
        public const string self_check = "self_check";
        /// <summary>確認更新日時</summary>
        public const string self_check_date = "self_check_date";
        /// <summary>確認更新者</summary>
        public const string self_check_name = "self_check_name";
        /// <summary>前段取り終了日時(計画)</summary>
        public const string plan_setup_end = "plan_setup_end";
        /// <summary>前段取り開始日時(計画)</summary>
        public const string plan_setup_start = "plan_setup_start";
        /// <summary>前段取り時間(計画)</summary>
        public const string plan_setup_working = "plan_setup_working";
        /// <summary>前段取り時間(計画)h</summary>
        public const string plan_setup_working_h = "plan_setup_working_h";
        /// <summary>仕様</summary>
        public const string specs = "specs";
        /// <summary>分割基底作業コード</summary>
        public const string split_base = "split_base";
        /// <summary>分割比率</summary>
        public const string split_ratio = "split_ratio";
        /// <summary>副資源1</summary>
        public const string sub_resource1 = "sub_resource1";
        /// <summary>副資源2</summary>
        public const string sub_resource2 = "sub_resource2";
        /// <summary>副資源3</summary>
        public const string sub_resource3 = "sub_resource3";
        /// <summary>後段取り終了日時(計画)</summary>
        public const string plan_teardown_end = "plan_teardown_end";
        /// <summary>後段取り開始日時(計画)</summary>
        public const string plan_teardown_start = "plan_teardown_start";
        /// <summary>後段取り時間(計画)</summary>
        public const string plan_teardown_working = "plan_teardown_working";
        /// <summary>後段取り時間(計画)h</summary>
        public const string plan_teardown_working_h = "plan_teardown_working_h";
        /// <summary>作業状況</summary>
        public const string work_status = "work_status";
        /// <summary>更新種別</summary>
        public const string update_type = "update_type";
        /// <summary>連番</summary>
        public const string seq = "seq";
        /// <summary>副資源キー</summary>
        public const string subtask_key = "subtask_key";
        /// <summary>副資源パート</summary>
        public const string subtask_part = "subtask_part";
        /// <summary>副資源</summary>
        public const string subtask_resource = "subtask_resource";
        /// <summary>副資源量</summary>
        public const string subtask_qty = "subtask_qty";
        /// <summary>副資源アンカー</summary>
        public const string subtask_anchor = "subtask_anchor";
        /// <summary>資源名</summary>
        public const string resource_exact_name = "resource_exact_name";
        /// <summary>資源種別</summary>
        public const string resource_type = "resource_type";
        /// <summary>グループフラグ</summary>
        public const string group_flag = "group_flag";
        /// <summary>所属グループ</summary>
        public const string member_of = "member_of";
        /// <summary>除外グループ</summary>
        public const string not_member_of = "not_member_of";
        /// <summary>ロケーションフラグ</summary>
        public const string is_location = "is_location";
        /// <summary>代理ロケーション</summary>
        public const string subst_loc = "subst_loc";
        /// <summary>時間メッシュ</summary>
        public const string time_mesh = "time_mesh";
        /// <summary>資源接続制約</summary>
        public const string connection = "connection";
        /// <summary>ラインキー</summary>
        public const string line_key = "line_key";
        /// <summary>セレクタ</summary>
        public const string selector = "selector";
        /// <summary>資源仕様制約</summary>
        public const string const_specs = "const_specs";
        /// <summary>資源数値仕様制約</summary>
        public const string const_numspecs = "const_numspecs";
        /// <summary>資源仕様</summary>
        public const string g_specs = "g_specs";
        /// <summary>資源数値仕様</summary>
        public const string g_numspecs = "g_numspecs";
        /// <summary>ボトルネック</summary>
        public const string bottleneck = "bottleneck";
        /// <summary>同時積み</summary>
        public const string simul_load = "simul_load";
        /// <summary>並行条件</summary>
        public const string cncnt_cond = "cncnt_cond";
        /// <summary>資源量係数</summary>
        public const string rq_coef = "rq_coef";
        /// <summary>占有可能</summary>
        public const string occupiable = "occupiable";
        /// <summary>班資源</summary>
        public const string squad = "squad";
        /// <summary>色</summary>
        public const string color = "color";
        /// <summary>オーダーバーコード</summary>
        public const string order_barcode = "order_barcode";
        /// <summary>資源コード</summary>
        public const string resource = "resource";
        /// <summary>所属資源</summary>
        public const string indent_resource = "indent_resource";
        /// <summary>原料数量</summary>
        public const string mtrl_qty = "mtrl_qty";
        /// <summary>原料品目</summary>
        public const string mtrl_item = "mtrl_item";
        /// <summary>原料キー</summary>
        public const string mtrl_key = "mtrl_key";
        /// <summary>出力品目</summary>
        public const string out_item = "out_item";
        /// <summary>出力作業</summary>
        public const string out_operation = "out_operation";
        /// <summary>リンクキー</summary>
        public const string out_key = "out_key";
        /// <summary>出力時間制約</summary>
        public const string out_timecs = "out_timecs";
        /// <summary>実績メモ有り</summary>
        public const string exists_result_memo = "exists_result_memo";
        /// <summary>ファイルID</summary>
        public const string file_stream_id = "file_stream_id";
        /// <summary>ファイルID1</summary>
        public const string file_stream_id1 = "file_stream_id1";
        /// <summary>ファイルID2</summary>
        public const string file_stream_id2 = "file_stream_id2";
        /// <summary>ファイルID3</summary>
        public const string file_stream_id3 = "file_stream_id3";
        /// <summary>ファイルパス</summary>
        public const string file_path = "file_path";
        /// <summary>ファイル更新日時</summary>
        public const string file_update_date = "file_update_date";
        /// <summary>ダウンロードURL</summary>
        public const string download_url = "download_url";
        /// <summary>発生日時</summary>
        public const string occur_date = "occur_date";
        /// <summary>不良内容コード</summary>
        public const string contents_cd = "contents_cd";
        /// <summary>不良内容メモ</summary>
        public const string contents_memo = "contents_memo";
        /// <summary>不良原因コード</summary>
        public const string cause_cd = "cause_cd";
        /// <summary>不良原因メモ</summary>
        public const string cause_memo = "cause_memo";
        /// <summary>処置コード</summary>
        public const string recovery_cd = "recovery_cd";
        /// <summary>処置メモ</summary>
        public const string recovery_memo = "recovery_memo";
        /// <summary>添付1</summary>
        public const string append_file1 = "append_file1";
        /// <summary>添付2</summary>
        public const string append_file2 = "append_file2";
        /// <summary>添付3</summary>
        public const string append_file3 = "append_file3";
        /// <summary>ナレッジ連関種類</summary>
        public const string relation_type_code = "relation_type_code";
        /// <summary>変更時ナレッジ連関種類</summary>
        public const string chg_relation_type_code = "chg_relation_type_code";
        /// <summary>種類</summary>
        public const string relation_type_name = "relation_type_name";
        /// <summary>連関値</summary>
        public const string relation_value = "relation_value";
        /// <summary>ナレッジ添付</summary>
        public const string klg_append_file = "klg_append_file";
        /// <summary>ナレッジ添付1</summary>
        public const string klg_append_file1 = "klg_append_file1";
        /// <summary>ナレッジ添付2</summary>
        public const string klg_append_file2 = "klg_append_file2";
        /// <summary>ナレッジ添付3</summary>
        public const string klg_append_file3 = "klg_append_file3";
        /// <summary>ナレッジ添付4</summary>
        public const string klg_append_file4 = "klg_append_file4";
        /// <summary>ナレッジ添付5</summary>
        public const string klg_append_file5 = "klg_append_file5";
        /// <summary>ナレッジ添付6</summary>
        public const string klg_append_file6 = "klg_append_file6";
        /// <summary>ナレッジ添付7</summary>
        public const string klg_append_file7 = "klg_append_file7";
        /// <summary>ナレッジ添付8</summary>
        public const string klg_append_file8 = "klg_append_file8";
        /// <summary>ナレッジ添付9</summary>
        public const string klg_append_file9 = "klg_append_file9";
        /// <summary>ナレッジ添付10</summary>
        public const string klg_append_file10 = "klg_append_file10";
        /// <summary>ナレッジファイルID1</summary>
        public const string klg_file_stream_id1 = "klg_file_stream_id1";
        /// <summary>ナレッジファイルID2</summary>
        public const string klg_file_stream_id2 = "klg_file_stream_id2";
        /// <summary>ナレッジファイルID3</summary>
        public const string klg_file_stream_id3 = "klg_file_stream_id3";
        /// <summary>ナレッジファイルID4</summary>
        public const string klg_file_stream_id4 = "klg_file_stream_id4";
        /// <summary>ナレッジファイルID5</summary>
        public const string klg_file_stream_id5 = "klg_file_stream_id5";
        /// <summary>ナレッジファイルID6</summary>
        public const string klg_file_stream_id6 = "klg_file_stream_id6";
        /// <summary>ナレッジファイルID7</summary>
        public const string klg_file_stream_id7 = "klg_file_stream_id7";
        /// <summary>ナレッジファイルID8</summary>
        public const string klg_file_stream_id8 = "klg_file_stream_id8";
        /// <summary>ナレッジファイルID9</summary>
        public const string klg_file_stream_id9 = "klg_file_stream_id9";
        /// <summary>ナレッジファイルID10</summary>
        public const string klg_file_stream_id10 = "klg_file_stream_id10";
        /// <summary>不良内容</summary>
        public const string contents_name = "contents_name";
        /// <summary>不良原因</summary>
        public const string cause_name = "cause_name";
        /// <summary>処置</summary>
        public const string recovery_name = "recovery_name";
        /// <summary>得意先コード</summary>
        public const string customer_cd = "customer_cd";
        /// <summary>郵便番号</summary>
        public const string postal_code = "postal_code";
        /// <summary>住所１</summary>
        public const string address1 = "address1";
        /// <summary>住所２</summary>
        public const string address2 = "address2";
        /// <summary>電話番号</summary>
        public const string tel = "tel";
        /// <summary>FAX番号</summary>
        public const string fax = "fax";
        /// <summary>締日</summary>
        public const string note1 = "note1";
        /// <summary>現品票_備考</summary>
        public const string note2 = "note2";
        /// <summary>備考3</summary>
        public const string note3 = "note3";
        /// <summary>備考4</summary>
        public const string note4 = "note4";
        /// <summary>備考5</summary>
        public const string note5 = "note5";
        /// <summary>選択副資源</summary>
        public const string sub_resource = "sub_resource";
        /// <summary>作業区分</summary>
        public const string work_div = "work_div";
        /// <summary>作業予実差</summary>
        public const string ope_diff = "ope_diff";
        /// <summary>作業予実差h</summary>
        public const string ope_diff_h = "ope_diff_h";
        /// <summary>前段取り予実差</summary>
        public const string setup_diff = "setup_diff";
        /// <summary>前段取り予実差h</summary>
        public const string setup_diff_h = "setup_diff_h";
        /// <summary>製造予実差</summary>
        public const string manu_diff = "manu_diff";
        /// <summary>製造予実差h</summary>
        public const string manu_diff_h = "manu_diff_h";
        /// <summary>後段取り予実差</summary>
        public const string teardown_diff = "teardown_diff";
        /// <summary>後段取り予実差h</summary>
        public const string teardown_diff_h = "teardown_diff_h";
        /// <summary>作業時間比</summary>
        public const string ope_rate = "ope_rate";
        /// <summary>前段取り時間比</summary>
        public const string setup_rate = "setup_rate";
        /// <summary>製造時間比</summary>
        public const string manu_rate = "manu_rate";
        /// <summary>後段取り時間比</summary>
        public const string teardown_rate = "teardown_rate";
        /// <summary>作業開始日時(実績)</summary>
        public const string result_ope_start = "result_ope_start";
        /// <summary>作業終了日時(実績)</summary>
        public const string result_ope_end = "result_ope_end";
        /// <summary>作業時間(実績)</summary>
        public const string result_ope_minutes = "result_ope_minutes";
        /// <summary>作業時間(実績)h</summary>
        public const string result_ope_hours = "result_ope_hours";
        /// <summary>間接作業ステータスコード</summary>
        public const string indirect_status = "indirect_status";
        /// <summary>間接作業区分コード</summary>
        public const string indirect_div_cd = "indirect_div_cd";
        /// <summary>間接作業時間</summary>
        public const string indirect_minutes = "indirect_minutes";
        /// <summary>間接作業時間h</summary>
        public const string indirect_hours = "indirect_hours";
        /// <summary>間接作業収集日時</summary>
        public const string indirect_collection = "indirect_collection";
        /// <summary>間接作業区分</summary>
        public const string indirect_div_name = "indirect_div_name";
        /// <summary>間接作業ステータス</summary>
        public const string indirect_status_name = "indirect_status_name";
        /// <summary>稼働状況</summary>
        public const string running_status = "running_status";
        /// <summary>仕掛中作業</summary>
        public const string running_operation = "running_operation";
        /// <summary>タイプ1</summary>
        public const string file_type1 = "file_type1";
        /// <summary>サイズ1</summary>
        public const string file_size1 = "file_size1";
        /// <summary>タイプ2</summary>
        public const string file_type2 = "file_type2";
        /// <summary>サイズ2</summary>
        public const string file_size2 = "file_size2";
        /// <summary>タイプ3</summary>
        public const string file_type3 = "file_type3";
        /// <summary>サイズ3</summary>
        public const string file_size3 = "file_size3";
        /// <summary>ナレッジファイルタイプ</summary>
        public const string klg_file_type = "klg_file_type";
        /// <summary>ナレッジファイルタイプ1</summary>
        public const string klg_file_type1 = "klg_file_type1";
        /// <summary>ナレッジファイルタイプ2</summary>
        public const string klg_file_type2 = "klg_file_type2";
        /// <summary>ナレッジファイルタイプ3</summary>
        public const string klg_file_type3 = "klg_file_type3";
        /// <summary>ナレッジファイルタイプ4</summary>
        public const string klg_file_type4 = "klg_file_type4";
        /// <summary>ナレッジファイルタイプ5</summary>
        public const string klg_file_type5 = "klg_file_type5";
        /// <summary>ナレッジファイルタイプ6</summary>
        public const string klg_file_type6 = "klg_file_type6";
        /// <summary>ナレッジファイルタイプ7</summary>
        public const string klg_file_type7 = "klg_file_type7";
        /// <summary>ナレッジファイルタイプ8</summary>
        public const string klg_file_type8 = "klg_file_type8";
        /// <summary>ナレッジファイルタイプ9</summary>
        public const string klg_file_type9 = "klg_file_type9";
        /// <summary>ナレッジファイルタイプ10</summary>
        public const string klg_file_type10 = "klg_file_type10";
        /// <summary>ナレッジファイルサイズ1</summary>
        public const string klg_file_size1 = "klg_file_size1";
        /// <summary>ナレッジファイルサイズ2</summary>
        public const string klg_file_size2 = "klg_file_size2";
        /// <summary>ナレッジファイルサイズ3</summary>
        public const string klg_file_size3 = "klg_file_size3";
        /// <summary>ナレッジファイルサイズ4</summary>
        public const string klg_file_size4 = "klg_file_size4";
        /// <summary>ナレッジファイルサイズ5</summary>
        public const string klg_file_size5 = "klg_file_size5";
        /// <summary>ナレッジファイルサイズ6</summary>
        public const string klg_file_size6 = "klg_file_size6";
        /// <summary>ナレッジファイルサイズ7</summary>
        public const string klg_file_size7 = "klg_file_size7";
        /// <summary>ナレッジファイルサイズ8</summary>
        public const string klg_file_size8 = "klg_file_size8";
        /// <summary>ナレッジファイルサイズ9</summary>
        public const string klg_file_size9 = "klg_file_size9";
        /// <summary>ナレッジファイルサイズ10</summary>
        public const string klg_file_size10 = "klg_file_size10";
        /// <summary>ナレッジメモ</summary>
        public const string klg_memo = "klg_memo";
        /// <summary>ナレッジメモ1</summary>
        public const string klg_memo1 = "klg_memo1";
        /// <summary>ナレッジメモ2</summary>
        public const string klg_memo2 = "klg_memo2";
        /// <summary>ナレッジメモ3</summary>
        public const string klg_memo3 = "klg_memo3";
        /// <summary>ナレッジメモ4</summary>
        public const string klg_memo4 = "klg_memo4";
        /// <summary>ナレッジメモ5</summary>
        public const string klg_memo5 = "klg_memo5";
        /// <summary>ナレッジメモ6</summary>
        public const string klg_memo6 = "klg_memo6";
        /// <summary>ナレッジメモ7</summary>
        public const string klg_memo7 = "klg_memo7";
        /// <summary>ナレッジメモ8</summary>
        public const string klg_memo8 = "klg_memo8";
        /// <summary>ナレッジメモ9</summary>
        public const string klg_memo9 = "klg_memo9";
        /// <summary>ナレッジメモ10</summary>
        public const string klg_memo10 = "klg_memo10";
        /// <summary>ナレッジキーワード</summary>
        public const string klg_keyword = "klg_keyword";
        /// <summary>ナレッジキーワード1</summary>
        public const string klg_keyword1 = "klg_keyword1";
        /// <summary>ナレッジキーワード2</summary>
        public const string klg_keyword2 = "klg_keyword2";
        /// <summary>ナレッジキーワード3</summary>
        public const string klg_keyword3 = "klg_keyword3";
        /// <summary>ナレッジキーワード4</summary>
        public const string klg_keyword4 = "klg_keyword4";
        /// <summary>ナレッジキーワード5</summary>
        public const string klg_keyword5 = "klg_keyword5";
        /// <summary>ナレッジキーワード6</summary>
        public const string klg_keyword6 = "klg_keyword6";
        /// <summary>ナレッジキーワード7</summary>
        public const string klg_keyword7 = "klg_keyword7";
        /// <summary>ナレッジキーワード8</summary>
        public const string klg_keyword8 = "klg_keyword8";
        /// <summary>ナレッジキーワード9</summary>
        public const string klg_keyword9 = "klg_keyword9";
        /// <summary>ナレッジキーワード10</summary>
        public const string klg_keyword10 = "klg_keyword10";
        /// <summary>完全一致</summary>
        public const string perfect_matching = "perfect_matching";
        /// <summary>検索対象</summary>
        public const string search_target = "search_target";
        /// <summary>キーワード</summary>
        public const string keyword = "keyword";
        /// <summary>メモ区分</summary>
        public const string memo_div = "memo_div";
        /// <summary>タイプ</summary>
        public const string file_type = "file_type";
        /// <summary>サイズ</summary>
        public const string file_size = "file_size";
        /// <summary>最終アクセス日時</summary>
        public const string last_access_date = "last_access_date";
        /// <summary>作業状況コード</summary>
        public const string work_status_cd = "work_status_cd";
        /// <summary>添付ファイル</summary>
        public const string append_file = "append_file";
        /// <summary>ナレッジカテゴリID</summary>
        public const string klg_category_id = "klg_category_id";
        /// <summary>ナレッジID</summary>
        public const string klg_id = "klg_id";
        /// <summary>親ナレッジカテゴリID</summary>
        public const string parent_id = "parent_id";
        /// <summary>親ナレッジカテゴリ名</summary>
        public const string parent_name = "parent_name";
        /// <summary>読取り権限</summary>
        public const string read = "read";
        /// <summary>書込み権限</summary>
        public const string write = "write";
        /// <summary>ナレッジカテゴリ名</summary>
        public const string klg_category_name = "klg_category_name";
        /// <summary>管理者メニューロック</summary>
        public const string admin_menu_lock = "admin_menu_lock";
        /// <summary>権限</summary>
        public const string authority_name = "authority_name";
        /// <summary>大文字小文字を区別しない</summary>
        public const string ignore_case = "ignore_case";
        /// <summary>子も検索対象とする。</summary>
        public const string recursive_search = "recursive_search";
        /// <summary>有効期間内のものを検索対象とする</summary>
        public const string validdate_search = "validdate_search";
        /// <summary>オーダー作業状況</summary>
        public const string order_work_status = "order_work_status";
        /// <summary>見込みレベル</summary>
        public const string prospect_name = "prospect_name";
        /// <summary>未仕掛</summary>
        public const string none_process = "none_process";
        /// <summary>仕掛中</summary>
        public const string in_process = "in_process";
        /// <summary>完了</summary>
        public const string finished = "finished";
        /// <summary>未計画オーダー</summary>
        public const string none_planning_order = "none_planning_order";
        /// <summary>保留オーダー</summary>
        public const string pending_order = "pending_order";
        /// <summary>本日作業</summary>
        public const string today_operation = "today_operation";
        /// <summary>パネルID</summary>
        public const string panel_id = "panel_id";
        /// <summary>タスクキー</summary>
        public const string task_key = "task_key";
        /// <summary>ファイルサイズ(byte)</summary>
        public const string file_size_byte = "file_size_byte";
        /// <summary>ファイルサイズ1(byte)</summary>
        public const string file_size_byte1 = "file_size_byte1";
        /// <summary>ファイルサイズ2(byte)</summary>
        public const string file_size_byte2 = "file_size_byte2";
        /// <summary>ファイルサイズ3(byte)</summary>
        public const string file_size_byte3 = "file_size_byte3";
        /// <summary>有無</summary>
        public const string existence = "existence";
        /// <summary>商品名(図番)</summary>
        public const string item_exact_name = "item_exact_name";
        /// <summary>ナレッジカテゴリパス</summary>
        public const string klg_category_path = "klg_category_path";
        /// <summary>値</summary>
        public const string value = "value";
        /// <summary>不良情報ステータス</summary>
        public const string failure_status = "failure_status";
        /// <summary>未割付オーダー</summary>
        public const string none_asign_order = "none_asign_order";
        /// <summary>保留フラグ</summary>
        public const string pending_flag = "pending_flag";
        /// <summary>所属品目</summary>
        public const string indent_item = "indent_item";
        /// <summary>オーダー種別コード</summary>
        public const string order_type = "order_type";
        /// <summary>オーダー種別名</summary>
        public const string order_type_name = "order_type_name";
        /// <summary>計画メモ</summary>
        public const string exists_plan_memo = "exists_plan_memo";
        /// <summary>作業指示取込フラグ</summary>
        public const string import_operation = "import_operation";
        /// <summary>品目マスタ取込フラグ</summary>
        public const string import_item = "import_item";
        /// <summary>資源マスタ取込フラグ</summary>
        public const string import_resource = "import_resource";
        /// <summary>工程マスタ取込フラグ</summary>
        public const string import_proc = "import_proc";
        /// <summary>予実比率有効</summary>
        public const string rate_enabled = "rate_enabled";
        /// <summary>対象パート</summary>
        public const string target_part = "target_part";
        /// <summary>上限比率</summary>
        public const string upper_num = "upper_num";
        /// <summary>下限比率</summary>
        public const string lower_num = "lower_num";
        /// <summary>抽出範囲</summary>
        public const string extract_bound = "extract_bound";
        /// <summary>主資源名</summary>
        public const string primary_resource_name = "primary_resource_name";
        /// <summary>合計</summary>
        public const string summary = "summary";
        /// <summary>数量１当りの時間(分)</summary>
        public const string per_one_unit = "per_one_unit";
        /// <summary>平均値</summary>
        public const string average = "average";
        /// <summary>中央値</summary>
        public const string median = "median";
        /// <summary>最大値</summary>
        public const string max = "max";
        /// <summary>最小値</summary>
        public const string min = "min";
        /// <summary>標準偏差</summary>
        public const string standard_deviation = "standard_deviation";
        /// <summary>作業1件当り</summary>
        public const string per_one_operation = "per_one_operation";
        /// <summary>工程コード</summary>
        public const string proc_cd = "proc_cd";
        /// <summary>工程名</summary>
        public const string proc_exact_name = "proc_exact_name";
        /// <summary>基準日時</summary>
        public const string base_date = "base_date";
        /// <summary>作業区分名</summary>
        public const string work_div_name = "work_div_name";
        /// <summary>作業</summary>
        public const string operation = "operation";
        /// <summary>副資源名</summary>
        public const string subtask_resource_name = "subtask_resource_name";
        /// <summary>表示期間（月）</summary>
        public const string visible_month = "visible_month";
        /// <summary>表示項目</summary>
        public const string visible_item = "visible_item";
        /// <summary>表示期間（日）</summary>
        public const string visible_date = "visible_date";
        /// <summary>更新間隔</summary>
        public const string update_interval = "update_interval";
        /// <summary>選択資源</summary>
        public const string selected_resource = "selected_resource";
        /// <summary>不良率</summary>
        public const string failure_rate = "failure_rate";
        /// <summary>項目数</summary>
        public const string number = "number";
        /// <summary>割合</summary>
        public const string ratio = "ratio";
        /// <summary>平均時間</summary>
        public const string average_hour = "average_hour";
        /// <summary>副資源1名</summary>
        public const string sub_resource1_name = "sub_resource1_name";
        /// <summary>副資源2名</summary>
        public const string sub_resource2_name = "sub_resource2_name";
        /// <summary>副資源3名</summary>
        public const string sub_resource3_name = "sub_resource3_name";
        /// <summary>選択副資源名</summary>
        public const string sub_resource_name = "sub_resource_name";
        /// <summary>カレンダー取込フラグ</summary>
        public const string import_calendar = "import_calendar";
        /// <summary>未入力</summary>
        public const string not_entered = "not_entered";
        /// <summary>作業パート</summary>
        public const string work_part = "work_part";
        /// <summary>明細区分コード</summary>
        public const string detail_div_cd = "detail_div_cd";
        /// <summary>明細日時</summary>
        public const string detail_date = "detail_date";
        /// <summary>明細時間</summary>
        public const string detail_minutes = "detail_minutes";
        /// <summary>作業パート名</summary>
        public const string work_part_name = "work_part_name";
        /// <summary>明細区分名</summary>
        public const string detail_div_name = "detail_div_name";
        /// <summary>[外注先名]</summary>
        public const string order_extcol1 = "order_extcol1";
        /// <summary>[摘要]</summary>
        public const string order_extcol2 = "order_extcol2";
        /// <summary>[注番]</summary>
        public const string order_extcol3 = "order_extcol3";
        /// <summary>[顧客支給品]</summary>
        public const string order_extcol4 = "order_extcol4";
        /// <summary>[PG変更数]</summary>
        public const string order_extcol5 = "order_extcol5";
        /// <summary>[単価]</summary>
        public const string order_extcol6 = "order_extcol6";
        /// <summary>[材寸]</summary>
        public const string order_extcol7 = "order_extcol7";
        /// <summary>[材質名]</summary>
        public const string order_extcol8 = "order_extcol8";
        /// <summary>[表面処理]</summary>
        public const string order_extcol9 = "order_extcol9";
        /// <summary>オーダー拡張列10</summary>
        public const string order_extcol10 = "order_extcol10";
        /// <summary>オーダー拡張列11</summary>
        public const string order_extcol11 = "order_extcol11";
        /// <summary>オーダー拡張列12</summary>
        public const string order_extcol12 = "order_extcol12";
        /// <summary>オーダー拡張列13</summary>
        public const string order_extcol13 = "order_extcol13";
        /// <summary>オーダー拡張列14</summary>
        public const string order_extcol14 = "order_extcol14";
        /// <summary>オーダー拡張列15</summary>
        public const string order_extcol15 = "order_extcol15";
        /// <summary>オーダー拡張列16</summary>
        public const string order_extcol16 = "order_extcol16";
        /// <summary>オーダー拡張列17</summary>
        public const string order_extcol17 = "order_extcol17";
        /// <summary>オーダー拡張列18</summary>
        public const string order_extcol18 = "order_extcol18";
        /// <summary>オーダー拡張列19</summary>
        public const string order_extcol19 = "order_extcol19";
        /// <summary>オーダー拡張列20</summary>
        public const string order_extcol20 = "order_extcol20";
        /// <summary>オーダー拡張列21</summary>
        public const string order_extcol21 = "order_extcol21";
        /// <summary>オーダー拡張列22</summary>
        public const string order_extcol22 = "order_extcol22";
        /// <summary>オーダー拡張列23</summary>
        public const string order_extcol23 = "order_extcol23";
        /// <summary>オーダー拡張列24</summary>
        public const string order_extcol24 = "order_extcol24";
        /// <summary>オーダー拡張列25</summary>
        public const string order_extcol25 = "order_extcol25";
        /// <summary>オーダー拡張列26</summary>
        public const string order_extcol26 = "order_extcol26";
        /// <summary>オーダー拡張列27</summary>
        public const string order_extcol27 = "order_extcol27";
        /// <summary>オーダー拡張列28</summary>
        public const string order_extcol28 = "order_extcol28";
        /// <summary>オーダー拡張列29</summary>
        public const string order_extcol29 = "order_extcol29";
        /// <summary>オーダー拡張列30</summary>
        public const string order_extcol30 = "order_extcol30";
        /// <summary>FS拡張列タイプ</summary>
        public const string fs_col_type = "fs_col_type";
        /// <summary>FS拡張列キー</summary>
        public const string fs_col_key = "fs_col_key";
        /// <summary>MES対応列</summary>
        public const string mes_map_col = "mes_map_col";
        /// <summary>作業バーコード</summary>
        public const string ope_barcode = "ope_barcode";
        /// <summary>バーコード</summary>
        public const string barcode = "barcode";
        /// <summary>資源バーコード</summary>
        public const string resource_barcode = "resource_barcode";
        /// <summary>資源種別名</summary>
        public const string resource_type_name = "resource_type_name";
        /// <summary>簡易作業時間バーコード</summary>
        public const string simpleworktime_barcode = "simpleworktime_barcode";
        /// <summary>簡易作業時間コード</summary>
        public const string simpleworktime_cd = "simpleworktime_cd";
        /// <summary>簡易作業時間名</summary>
        public const string simpleworktime_name = "simpleworktime_name";
        /// <summary>オーダー名(更新用)</summary>
        public const string order_exact_name_for_update = "order_exact_name_for_update";
        /// <summary>実績数量(出力リンク単位)</summary>
        public const string links = "links";
        /// <summary>集計対象</summary>
        public const string summary_target = "summary_target";
        /// <summary>オーダー猶予日数(ソート用)</summary>
        public const string order_postponement_for_sort = "order_postponement_for_sort";
        /// <summary>１時間当たりの数量</summary>
        public const string per_one_hour = "per_one_hour";
        /// <summary>作業開始日時</summary>
        public const string ope_start = "ope_start";
        /// <summary>作業終了日時</summary>
        public const string ope_end = "ope_end";
        /// <summary>作業時間</summary>
        public const string ope_working = "ope_working";
        /// <summary>作業時間h</summary>
        public const string ope_working_h = "ope_working_h";
        /// <summary>前段取り終了日時</summary>
        public const string setup_end = "setup_end";
        /// <summary>前段取り開始日時</summary>
        public const string setup_start = "setup_start";
        /// <summary>前段取り時間</summary>
        public const string setup_working = "setup_working";
        /// <summary>前段取り時間h</summary>
        public const string setup_working_h = "setup_working_h";
        /// <summary>製造終了日時</summary>
        public const string manu_end = "manu_end";
        /// <summary>製造開始日時</summary>
        public const string manu_start = "manu_start";
        /// <summary>製造時間</summary>
        public const string manu_working = "manu_working";
        /// <summary>製造時間h</summary>
        public const string manu_working_h = "manu_working_h";
        /// <summary>後段取り終了日時</summary>
        public const string teardown_end = "teardown_end";
        /// <summary>後段取り開始日時</summary>
        public const string teardown_start = "teardown_start";
        /// <summary>後段取り時間</summary>
        public const string teardown_working = "teardown_working";
        /// <summary>後段取り時間h</summary>
        public const string teardown_working_h = "teardown_working_h";
        /// <summary>添付ファイル保存場所</summary>
        public const string path_locator = "path_locator";
        /// <summary>正式名称</summary>
        public const string exact_name = "exact_name";
        /// <summary>実績利用資源名</summary>
        public const string result_resources_name = "result_resources_name";
        /// <summary>WindowsPC版利用権限</summary>
        public const string is_pc = "is_pc";
        /// <summary>メールアドレス</summary>
        public const string email = "email";
        /// <summary>アラート種別コード</summary>
        public const string alert_type_cd = "alert_type_cd";
        /// <summary>閾値</summary>
        public const string boader = "boader";
        /// <summary>件名</summary>
        public const string subject = "subject";
        /// <summary>品目コード群</summary>
        public const string items = "items";
        /// <summary>品目名群</summary>
        public const string item_exact_names = "item_exact_names";
        /// <summary>最終送信日時</summary>
        public const string send_date = "send_date";
        /// <summary>得意先コード群</summary>
        public const string customer_cds = "customer_cds";
        /// <summary>得意先名群</summary>
        public const string customer_names = "customer_names";
        /// <summary>オーダーコード群</summary>
        public const string order_cds = "order_cds";
        /// <summary>オーダー名群</summary>
        public const string order_exact_names = "order_exact_names";
        /// <summary>有効期間開始日時</summary>
        public const string valid_start = "valid_start";
        /// <summary>有効期間終了日時</summary>
        public const string valid_end = "valid_end";
        /// <summary>全文検索対象フラグ</summary>
        public const string fts_flg = "fts_flg";
        /// <summary>商品名</summary>
        public const string item_code = "item_code";
        /// <summary>品目名称</summary>
        public const string item_name = "item_name";
        /// <summary>リピート</summary>
        public const string repeat_flag = "repeat_flag";
        /// <summary>PG変更有り</summary>
        public const string conv_pg_flag = "conv_pg_flag";
        /// <summary>引合</summary>
        public const string inquiry_flag = "inquiry_flag";
        /// <summary>材質コード</summary>
        public const string material_code = "material_code";
        /// <summary>材寸</summary>
        public const string material_size = "material_size";
        /// <summary>数量単位コード</summary>
        public const string num_unit_code = "num_unit_code";
        /// <summary>場所コード</summary>
        public const string location_code = "location_code";
        /// <summary>品目グループコード</summary>
        public const string item_group_code = "item_group_code";
        /// <summary>難易度</summary>
        public const string difficulty = "difficulty";
        /// <summary>厚さ</summary>
        public const string item_thickness = "item_thickness";
        /// <summary>巾</summary>
        public const string item_width = "item_width";
        /// <summary>長さ</summary>
        public const string item_length = "item_length";
        /// <summary>面取り加工コード</summary>
        public const string chamfer_type_code = "chamfer_type_code";
        /// <summary>面取り加工名</summary>
        public const string chamfer_type_name = "chamfer_type_name";
        /// <summary>全体備考</summary>
        public const string item_note1 = "item_note1";
        /// <summary>MH備考</summary>
        public const string item_note2 = "item_note2";
        /// <summary>特記事項</summary>
        public const string item_note3 = "item_note3";
        /// <summary>MC備考</summary>
        public const string item_note4 = "item_note4";
        /// <summary>品目備考</summary>
        public const string item_note5 = "item_note5";
        /// <summary>品目備考1朱書き</summary>
        public const string item_note1_reddish = "item_note1_reddish";
        /// <summary>品目備考2朱書き</summary>
        public const string item_note2_reddish = "item_note2_reddish";
        /// <summary>品目備考3朱書き</summary>
        public const string item_note3_reddish = "item_note3_reddish";
        /// <summary>品目備考4朱書き</summary>
        public const string item_note4_reddish = "item_note4_reddish";
        /// <summary>品目備考5朱書き</summary>
        public const string item_note5_reddish = "item_note5_reddish";
        /// <summary>得意先ｺｰﾄﾞ</summary>
        public const string item_ext1 = "item_ext1";
        /// <summary>表面処理</summary>
        public const string item_ext2 = "item_ext2";
        /// <summary>工順</summary>
        public const string proc_seq = "proc_seq";
        /// <summary>工程時間</summary>
        public const string proc_time = "proc_time";
        /// <summary>時間単位コード</summary>
        public const string time_unit_code = "time_unit_code";
        /// <summary>時間単位</summary>
        public const string time_unit_name = "time_unit_name";
        /// <summary>NCプログラムNo</summary>
        public const string nc_pgno = "nc_pgno";
        /// <summary>NCプログラムNo1</summary>
        public const string nc_pgno1 = "nc_pgno1";
        /// <summary>NCプログラムNo2</summary>
        public const string nc_pgno2 = "nc_pgno2";
        /// <summary>NCプログラムNo3</summary>
        public const string nc_pgno3 = "nc_pgno3";
        /// <summary>NCプログラムNo4</summary>
        public const string nc_pgno4 = "nc_pgno4";
        /// <summary>NCプログラムNo5</summary>
        public const string nc_pgno5 = "nc_pgno5";
        /// <summary>NCプログラムNo6</summary>
        public const string nc_pgno6 = "nc_pgno6";
        /// <summary>NCプログラムNo7</summary>
        public const string nc_pgno7 = "nc_pgno7";
        /// <summary>NCプログラムNo8</summary>
        public const string nc_pgno8 = "nc_pgno8";
        /// <summary>NCプログラムNo9</summary>
        public const string nc_pgno9 = "nc_pgno9";
        /// <summary>NCプログラムNo10</summary>
        public const string nc_pgno10 = "nc_pgno10";
        /// <summary>NCプログラムNo11</summary>
        public const string nc_pgno11 = "nc_pgno11";
        /// <summary>NCプログラムNo12</summary>
        public const string nc_pgno12 = "nc_pgno12";
        /// <summary>NCプログラムNo13</summary>
        public const string nc_pgno13 = "nc_pgno13";
        /// <summary>NCプログラムNo14</summary>
        public const string nc_pgno14 = "nc_pgno14";
        /// <summary>NCプログラムNo15</summary>
        public const string nc_pgno15 = "nc_pgno15";
        /// <summary>NCプログラムNo16</summary>
        public const string nc_pgno16 = "nc_pgno16";
        /// <summary>NCプログラムNo17</summary>
        public const string nc_pgno17 = "nc_pgno17";
        /// <summary>NCプログラムNo18</summary>
        public const string nc_pgno18 = "nc_pgno18";
        /// <summary>NCプログラムNo19</summary>
        public const string nc_pgno19 = "nc_pgno19";
        /// <summary>NCプログラムNo20</summary>
        public const string nc_pgno20 = "nc_pgno20";
        /// <summary>NCプログラムNo21</summary>
        public const string nc_pgno21 = "nc_pgno21";
        /// <summary>NCプログラムNo22</summary>
        public const string nc_pgno22 = "nc_pgno22";
        /// <summary>NCプログラムNo23</summary>
        public const string nc_pgno23 = "nc_pgno23";
        /// <summary>NCプログラムNo24</summary>
        public const string nc_pgno24 = "nc_pgno24";
        /// <summary>NCプログラムNo25</summary>
        public const string nc_pgno25 = "nc_pgno25";
        /// <summary>NCプログラムNo26</summary>
        public const string nc_pgno26 = "nc_pgno26";
        /// <summary>NCプログラムNo27</summary>
        public const string nc_pgno27 = "nc_pgno27";
        /// <summary>NCプログラムNo28</summary>
        public const string nc_pgno28 = "nc_pgno28";
        /// <summary>NCプログラムNo29</summary>
        public const string nc_pgno29 = "nc_pgno29";
        /// <summary>NCプログラムNo30</summary>
        public const string nc_pgno30 = "nc_pgno30";
        /// <summary>NCプログラムNo31</summary>
        public const string nc_pgno31 = "nc_pgno31";
        /// <summary>NCプログラムNo32</summary>
        public const string nc_pgno32 = "nc_pgno32";
        /// <summary>NCプログラムNo33</summary>
        public const string nc_pgno33 = "nc_pgno33";
        /// <summary>NCプログラムNo34</summary>
        public const string nc_pgno34 = "nc_pgno34";
        /// <summary>NCプログラムNo35</summary>
        public const string nc_pgno35 = "nc_pgno35";
        /// <summary>NCプログラムNo36</summary>
        public const string nc_pgno36 = "nc_pgno36";
        /// <summary>NCプログラムNo37</summary>
        public const string nc_pgno37 = "nc_pgno37";
        /// <summary>NCプログラムNo38</summary>
        public const string nc_pgno38 = "nc_pgno38";
        /// <summary>NCプログラムNo39</summary>
        public const string nc_pgno39 = "nc_pgno39";
        /// <summary>NCプログラムNo40</summary>
        public const string nc_pgno40 = "nc_pgno40";
        /// <summary>NCプログラムNo41</summary>
        public const string nc_pgno41 = "nc_pgno41";
        /// <summary>NCプログラムNo42</summary>
        public const string nc_pgno42 = "nc_pgno42";
        /// <summary>NCプログラムNo43</summary>
        public const string nc_pgno43 = "nc_pgno43";
        /// <summary>NCプログラムNo44</summary>
        public const string nc_pgno44 = "nc_pgno44";
        /// <summary>NCプログラムNo45</summary>
        public const string nc_pgno45 = "nc_pgno45";
        /// <summary>工程1</summary>
        public const string proc_cd1 = "proc_cd1";
        /// <summary>工程2</summary>
        public const string proc_cd2 = "proc_cd2";
        /// <summary>工程3</summary>
        public const string proc_cd3 = "proc_cd3";
        /// <summary>工程4</summary>
        public const string proc_cd4 = "proc_cd4";
        /// <summary>工程5</summary>
        public const string proc_cd5 = "proc_cd5";
        /// <summary>工程6</summary>
        public const string proc_cd6 = "proc_cd6";
        /// <summary>工程7</summary>
        public const string proc_cd7 = "proc_cd7";
        /// <summary>工程8</summary>
        public const string proc_cd8 = "proc_cd8";
        /// <summary>工程9</summary>
        public const string proc_cd9 = "proc_cd9";
        /// <summary>工程10</summary>
        public const string proc_cd10 = "proc_cd10";
        /// <summary>工程11</summary>
        public const string proc_cd11 = "proc_cd11";
        /// <summary>工程12</summary>
        public const string proc_cd12 = "proc_cd12";
        /// <summary>工程13</summary>
        public const string proc_cd13 = "proc_cd13";
        /// <summary>工程14</summary>
        public const string proc_cd14 = "proc_cd14";
        /// <summary>工程15</summary>
        public const string proc_cd15 = "proc_cd15";
        /// <summary>工程16</summary>
        public const string proc_cd16 = "proc_cd16";
        /// <summary>NCフォーマットコード</summary>
        public const string nc_format_code = "nc_format_code";
        /// <summary>NCプログラムパス</summary>
        public const string nc_pg_path = "nc_pg_path";
        /// <summary>工程時間1</summary>
        public const string proc_time_name1 = "proc_time_name1";
        /// <summary>工程時間2</summary>
        public const string proc_time_name2 = "proc_time_name2";
        /// <summary>工程時間3</summary>
        public const string proc_time_name3 = "proc_time_name3";
        /// <summary>工程時間4</summary>
        public const string proc_time_name4 = "proc_time_name4";
        /// <summary>工程時間5</summary>
        public const string proc_time_name5 = "proc_time_name5";
        /// <summary>工程時間6</summary>
        public const string proc_time_name6 = "proc_time_name6";
        /// <summary>工程時間7</summary>
        public const string proc_time_name7 = "proc_time_name7";
        /// <summary>工程時間8</summary>
        public const string proc_time_name8 = "proc_time_name8";
        /// <summary>工程時間9</summary>
        public const string proc_time_name9 = "proc_time_name9";
        /// <summary>工程時間10</summary>
        public const string proc_time_name10 = "proc_time_name10";
        /// <summary>工程時間11</summary>
        public const string proc_time_name11 = "proc_time_name11";
        /// <summary>工程時間12</summary>
        public const string proc_time_name12 = "proc_time_name12";
        /// <summary>工程時間13</summary>
        public const string proc_time_name13 = "proc_time_name13";
        /// <summary>工程時間14</summary>
        public const string proc_time_name14 = "proc_time_name14";
        /// <summary>工程時間15</summary>
        public const string proc_time_name15 = "proc_time_name15";
        /// <summary>工程時間16</summary>
        public const string proc_time_name16 = "proc_time_name16";
        /// <summary>工程時間1(数値)</summary>
        public const string proc_time1 = "proc_time1";
        /// <summary>工程時間2(数値)</summary>
        public const string proc_time2 = "proc_time2";
        /// <summary>工程時間3(数値)</summary>
        public const string proc_time3 = "proc_time3";
        /// <summary>工程時間4(数値)</summary>
        public const string proc_time4 = "proc_time4";
        /// <summary>工程時間5(数値)</summary>
        public const string proc_time5 = "proc_time5";
        /// <summary>工程時間6(数値)</summary>
        public const string proc_time6 = "proc_time6";
        /// <summary>工程時間7(数値)</summary>
        public const string proc_time7 = "proc_time7";
        /// <summary>工程時間8(数値)</summary>
        public const string proc_time8 = "proc_time8";
        /// <summary>工程時間9(数値)</summary>
        public const string proc_time9 = "proc_time9";
        /// <summary>工程時間10(数値)</summary>
        public const string proc_time10 = "proc_time10";
        /// <summary>工程時間11(数値)</summary>
        public const string proc_time11 = "proc_time11";
        /// <summary>工程時間12(数値)</summary>
        public const string proc_time12 = "proc_time12";
        /// <summary>工程時間13(数値)</summary>
        public const string proc_time13 = "proc_time13";
        /// <summary>工程時間14(数値)</summary>
        public const string proc_time14 = "proc_time14";
        /// <summary>工程時間15(数値)</summary>
        public const string proc_time15 = "proc_time15";
        /// <summary>工程時間16(数値)</summary>
        public const string proc_time16 = "proc_time16";
        /// <summary>有無名称</summary>
        public const string existence_name = "existence_name";
        /// <summary>パネル表示グループ名称</summary>
        public const string panel_disp_group_name = "panel_disp_group_name";
        /// <summary>グループのみ検索対象フラグ</summary>
        public const string only_group_flg = "only_group_flg";
        /// <summary>全体備考(文字数)</summary>
        public const string item_note1_length = "item_note1_length";
        /// <summary>MH備考(文字数)</summary>
        public const string item_note2_length = "item_note2_length";
        /// <summary>特記事項(文字数)</summary>
        public const string item_note3_length = "item_note3_length";
        /// <summary>MC備考(文字数)</summary>
        public const string item_note4_length = "item_note4_length";
        /// <summary>品目備考(文字数)</summary>
        public const string item_note5_length = "item_note5_length";
        /// <summary>工具連番</summary>
        public const string tool_seq = "tool_seq";
        /// <summary>工具注記</summary>
        public const string tool_note_code = "tool_note_code";
        /// <summary>T番号</summary>
        public const string t_no = "t_no";
        /// <summary>工具名</summary>
        public const string tool_name = "tool_name";
        /// <summary>H番号</summary>
        public const string h_no = "h_no";
        /// <summary>D番号</summary>
        public const string d_no = "d_no";
        /// <summary>工具備考1</summary>
        public const string tool_note1 = "tool_note1";
        /// <summary>工具備考2</summary>
        public const string tool_note2 = "tool_note2";
        /// <summary>工具備考3</summary>
        public const string tool_note3 = "tool_note3";
        /// <summary>工具備考4</summary>
        public const string tool_note4 = "tool_note4";
        /// <summary>工具備考5</summary>
        public const string tool_note5 = "tool_note5";
        /// <summary>T番号数</summary>
        public const string tool_count = "tool_count";
        /// <summary>工具注記名</summary>
        public const string tool_note_name = "tool_note_name";
        /// <summary>仕入先コード</summary>
        public const string supplier_cd = "supplier_cd";
        /// <summary>材料仕入先選択</summary>
        public const string material_supplier_cellbutton = "material_supplier_cellbutton";
        /// <summary>仕入先名</summary>
        public const string supplier_name = "supplier_name";
        /// <summary>仕入先備考1</summary>
        public const string supplier_note1 = "supplier_note1";
        /// <summary>仕入先備考2</summary>
        public const string supplier_note2 = "supplier_note2";
        /// <summary>仕入先備考3</summary>
        public const string supplier_note3 = "supplier_note3";
        /// <summary>仕入先備考4</summary>
        public const string supplier_note4 = "supplier_note4";
        /// <summary>仕入先備考5</summary>
        public const string supplier_note5 = "supplier_note5";
        /// <summary>仕入先連関コード</summary>
        public const string supplier_relation_cd = "supplier_relation_cd";
        /// <summary>表面処理コード</summary>
        public const string surface_cd = "surface_cd";
        /// <summary>表面処理選択</summary>
        public const string surface_cellbutton = "surface_cellbutton";
        /// <summary>表面処理名</summary>
        public const string surface_name = "surface_name";
        /// <summary>表面処理備考1</summary>
        public const string surface_note1 = "surface_note1";
        /// <summary>表面処理備考2</summary>
        public const string surface_note2 = "surface_note2";
        /// <summary>表面処理備考3</summary>
        public const string surface_note3 = "surface_note3";
        /// <summary>表面処理備考4</summary>
        public const string surface_note4 = "surface_note4";
        /// <summary>表面処理備考5</summary>
        public const string surface_note5 = "surface_note5";
        /// <summary>材質名</summary>
        public const string material_name = "material_name";
        /// <summary>標準単価</summary>
        public const string standard_unit = "standard_unit";
        /// <summary>材質備考1</summary>
        public const string material_note1 = "material_note1";
        /// <summary>材質備考2</summary>
        public const string material_note2 = "material_note2";
        /// <summary>材質備考3</summary>
        public const string material_note3 = "material_note3";
        /// <summary>材質備考4</summary>
        public const string material_note4 = "material_note4";
        /// <summary>材質備考5</summary>
        public const string material_note5 = "material_note5";
        /// <summary>受注ID</summary>
        public const string sales_order_id = "sales_order_id";
        /// <summary>受注正式名称</summary>
        public const string sales_order_name = "sales_order_name";
        /// <summary>受注日</summary>
        public const string sales_order_date = "sales_order_date";
        /// <summary>得意先担当者</summary>
        public const string customer_person_name = "customer_person_name";
        /// <summary>顧客希望納期</summary>
        public const string customer_delivery_date = "customer_delivery_date";
        /// <summary>回答納期</summary>
        public const string delivery_date = "delivery_date";
        /// <summary>受注額合計</summary>
        public const string amount_sum = "amount_sum";
        /// <summary>値引額合計</summary>
        public const string discount_sum = "discount_sum";
        /// <summary>納品方法コード</summary>
        public const string delivery_method_cd = "delivery_method_cd";
        /// <summary>納品方法</summary>
        public const string delivery_method_name = "delivery_method_name";
        /// <summary>発送予定日</summary>
        public const string shipping_date = "shipping_date";
        /// <summary>社内注文id</summary>
        public const string company_order_id = "company_order_id";
        /// <summary>外注依頼先コード</summary>
        public const string bpartner_cd = "bpartner_cd";
        /// <summary>外注依頼先名</summary>
        public const string bpartner_name = "bpartner_name";
        /// <summary>加工納期</summary>
        public const string processing_delivery_date = "processing_delivery_date";
        /// <summary>材料発注予定日</summary>
        public const string material_order_date = "material_order_date";
        /// <summary>受注備考1</summary>
        public const string sales_order_note1 = "sales_order_note1";
        /// <summary>受注備考2</summary>
        public const string sales_order_note2 = "sales_order_note2";
        /// <summary>受注備考3</summary>
        public const string sales_order_note3 = "sales_order_note3";
        /// <summary>受注備考4</summary>
        public const string sales_order_note4 = "sales_order_note4";
        /// <summary>受注備考5</summary>
        public const string sales_order_note5 = "sales_order_note5";
        /// <summary>受注明細正式名称</summary>
        public const string sales_orderline_name = "sales_orderline_name";
        /// <summary>明細種別コード</summary>
        public const string sales_orderline_type_cd = "sales_orderline_type_cd";
        /// <summary>受注明細ステータス</summary>
        public const string sales_orderline_status = "sales_orderline_status";
        /// <summary>保留</summary>	
        public const string pending = "pending";
        /// <summary>売上対象フラグ</summary>
        public const string sales_target_flg = "sales_target_flg";
        /// <summary>品目有効期間開始日</summary>
        public const string item_valid_start = "item_valid_start";
        /// <summary>治具</summary>
        public const string jig_flg = "jig_flg";
        /// <summary>その他非品目名称</summary>
        public const string non_item_name = "non_item_name";
        /// <summary>品目数量</summary>	
        public const string item_qty = "item_qty";
        /// <summary>品目単価</summary>	
        public const string item_selling_unit = "item_selling_unit";
        /// <summary>品目値引額</summary>
        public const string item_discount = "item_discount";
        /// <summary>受注額行合計</summary>
        public const string sales_orderline_amount_sum = "sales_orderline_amount_sum";
        /// <summary>摘要</summary>
        public const string sales_orderline_summary = "sales_orderline_summary";
        /// <summary>材料摘要</summary>
        public const string material_summary = "material_summary";
        /// <summary>材料数量</summary>
        public const string material_qty = "material_qty";
        /// <summary>材料単価</summary>
        public const string material_unit = "material_unit";
        /// <summary>切断指示</summary>
        public const string cutting_instructions = "cutting_instructions";
        /// <summary>顧客支給フラグ</summary>
        public const string customer_payment_flg = "customer_payment_flg";
        /// <summary>表面処理依頼先コード</summary>
        public const string surface_supplier_cd = "surface_supplier_cd";
        /// <summary>表面処理依頼先選択</summary>
        public const string surface_supplier_cellbutton = "surface_supplier_cellbutton";
        /// <summary>表面処理単価</summary>
        public const string surface_unit = "surface_unit";
        /// <summary>ヘリサート有無</summary>
        public const string is_helicoid = "is_helicoid";
        /// <summary>受注明細備考1</summary>
        public const string sales_orderline_note1 = "sales_orderline_note1";
        /// <summary>受注明細備考2</summary>
        public const string sales_orderline_note2 = "sales_orderline_note2";
        /// <summary>受注明細備考3</summary>
        public const string sales_orderline_note3 = "sales_orderline_note3";
        /// <summary>受注明細備考4</summary>
        public const string sales_orderline_note4 = "sales_orderline_note4";
        /// <summary>受注明細備考5</summary>
        public const string sales_orderline_note5 = "sales_orderline_note5";
        /// <summary>表面処理依頼先</summary>
        public const string surface_supplier_name = "surface_supplier_name";
        /// <summary>[工程]</summary>
        public const string proc_all_name = "proc_all_name";
        /// <summary>帳票名</summary>
        public const string report_name = "report_name";
        /// <summary>計画メモ登録状況も検索する</summary>
        public const string exists_plan_memo_status = "exists_plan_memo_status";
        /// <summary>工程レベル</summary>
        public const string proc_level = "proc_level";
        /// <summary>作業実績拡張列1</summary>
        public const string result_extcol1 = "result_extcol1";
        /// <summary>作業実績拡張列2</summary>
        public const string result_extcol2 = "result_extcol2";
        /// <summary>作業実績拡張列3</summary>
        public const string result_extcol3 = "result_extcol3";
        /// <summary>作業実績拡張列4</summary>
        public const string result_extcol4 = "result_extcol4";
        /// <summary>作業実績拡張列5</summary>
        public const string result_extcol5 = "result_extcol5";
        /// <summary>作業実績拡張列6</summary>
        public const string result_extcol6 = "result_extcol6";
        /// <summary>作業実績拡張列7</summary>
        public const string result_extcol7 = "result_extcol7";
        /// <summary>作業実績拡張列8</summary>
        public const string result_extcol8 = "result_extcol8";
        /// <summary>作業実績拡張列9</summary>
        public const string result_extcol9 = "result_extcol9";
        /// <summary>作業実績拡張列10</summary>
        public const string result_extcol10 = "result_extcol10";
        /// <summary>計画外カテゴリ</summary>
        public const string unplanned_category = "unplanned_category";
        /// <summary>計画外カテゴリ名称</summary>
        public const string unplanned_category_name = "unplanned_category_name";
        /// <summary>完成品オーダーコード</summary>
        public const string product_order_cd = "product_order_cd";
        /// <summary>明細種別名</summary>
        public const string sales_orderline_type_name = "sales_orderline_type_name";
        /// <summary>明細ステータス名</summary>
        public const string sales_orderline_status_name = "sales_orderline_status_name";
        /// <summary>品目選択</summary>	
        public const string item_code_cellbutton = "item_code_cellbutton";
        /// <summary>倉庫ID</summary>
        public const string repository_id = "repository_id";
        /// <summary>倉庫名</summary>
        public const string repository_name = "repository_name";
        /// <summary>場所コード</summary>
        public const string location_cd = "location_cd";
        /// <summary>棚卸宣言時刻</summary>
        public const string inventory_start_date = "inventory_start_date";
        /// <summary>収容能力</summary>
        public const string capacity = "capacity";
        /// <summary>ロットNo</summary>
        public const string internal_lot_no = "internal_lot_no";
        /// <summary>在庫数</summary>
        public const string stock_qty = "stock_qty";
        /// <summary>外部ロットNo</summary>
        public const string external_lot_no = "external_lot_no";
        /// <summary>ロットステータスコード</summary>
        public const string lot_status_cd = "lot_status_cd";
        /// <summary>ロットステータス</summary>
        public const string lot_status_name = "lot_status_name";
        /// <summary>品質保持期限日</summary>
        public const string quality_deadline = "quality_deadline";
        /// <summary>入庫数</summary>	
        public const string in_stock_qty = "in_stock_qty";
        /// <summary>出庫数</summary>	
        public const string out_stock_qty = "out_stock_qty";
        /// <summary>在庫廃棄数</summary>	
        public const string discard_stock_qty = "discard_stock_qty";
        /// <summary>入庫日</summary>	
        public const string in_stock_date = "in_stock_date";
        /// <summary>出庫日</summary>	
        public const string out_stock_date = "out_stock_date";
        /// <summary>廃棄日</summary>	
        public const string discard_stock_date = "discard_stock_date";
        /// <summary>入出庫ID</summary>
        public const string inout_id = "inout_id";
        /// <summary>入出庫区分コード</summary>
        public const string inout_div_cd = "inout_div_cd";
        /// <summary>入出庫区分</summary>
        public const string inout_div_name = "inout_div_name";
        /// <summary>入出庫日時</summary>
        public const string inout_date = "inout_date";
        /// <summary>操作数</summary>
        public const string operation_qty = "operation_qty";
        /// <summary>操作後数</summary>
        public const string after_operation_qty = "after_operation_qty";
        /// <summary>実棚数</summary>
        public const string real_qty = "real_qty";
        /// <summary>実棚時刻</summary>
        public const string count_date = "count_date";
        /// <summary>棚卸宣言時刻在庫数</summary>
        public const string start_qty = "start_qty";
        /// <summary>論理比較在庫数</summary>
        public const string logical_cmp_qty = "logical_cmp_qty";
        /// <summary>差異数</summary>
        public const string diff_qty = "diff_qty";
        /// <summary>差異原因コード</summary>
        public const string diff_cause_cd = "diff_cause_cd";
        /// <summary>差異原因</summary>
        public const string diff_cause_name = "diff_cause_name";
        /// <summary>棚卸作業状況コード</summary>
        public const string inventory_work_status_cd = "inventory_work_status_cd";
        /// <summary>棚卸作業状況</summary>
        public const string inventory_work_status = "inventory_work_status";
        /// <summary>棚卸予定日</summary>
        public const string inventory_planned_date = "inventory_planned_date";
        /// <summary>棚卸ID</summary>
        public const string inventory_id = "inventory_id";
        /// <summary>廃棄理由コード</summary>
        public const string discard_cause_cd = "discard_cause_cd";
        /// <summary>廃棄理由</summary>
        public const string discard_cause_name = "discard_cause_name";
        /// <summary>部区</summary>
        public const string part_class_cd = "part_class_cd";
        /// <summary>部品区分名称</summary>
        public const string part_class_name = "part_class_name";
        /// <summary>部品区分備考</summary>
        public const string part_class_note = "part_class_note";
        /// <summary>直接リンクファイル名称</summary> 
        public const string direct_link_file_name = "direct_link_file_name";
        /// <summary>ナレッジ直接リンク</summary>
        public const string klg_direct_link_file_name = "klg_direct_link_file_name";
        /// <summary>ナレッジ直接リンク1</summary>
        public const string klg_direct_link_file_name1 = "klg_direct_link_file_name1";
        /// <summary>ナレッジ直接リンク2</summary>
        public const string klg_direct_link_file_name2 = "klg_direct_link_file_name2";
        /// <summary>ナレッジ直接リンク3</summary>
        public const string klg_direct_link_file_name3 = "klg_direct_link_file_name3";
        /// <summary>ナレッジ直接リンク4</summary>
        public const string klg_direct_link_file_name4 = "klg_direct_link_file_name4";
        /// <summary>ナレッジ直接リンク5</summary>
        public const string klg_direct_link_file_name5 = "klg_direct_link_file_name5";
        /// <summary>ナレッジ直接リンク6</summary>
        public const string klg_direct_link_file_name6 = "klg_direct_link_file_name6";
        /// <summary>ナレッジ直接リンク7</summary>
        public const string klg_direct_link_file_name7 = "klg_direct_link_file_name7";
        /// <summary>ナレッジ直接リンク8</summary>
        public const string klg_direct_link_file_name8 = "klg_direct_link_file_name8";
        /// <summary>ナレッジ直接リンク9</summary>
        public const string klg_direct_link_file_name9 = "klg_direct_link_file_name9";
        /// <summary>ナレッジ直接リンク10</summary>
        public const string klg_direct_link_file_name10 = "klg_direct_link_file_name10";
        /// <summary>1個あたりの重量（kg)</summary>
        public const string unit_weight = "unit_weight";
        /// <summary>Kg単価</summary>
        public const string kilo_cost = "kilo_cost";
        /// <summary>Kg単価(指値)</summary>
        public const string edited_kilo_cost = "edited_kilo_cost";
        /// <summary>切断のみの材料費</summary>
        public const string material_cost_cutting = "material_cost_cutting";
        /// <summary>その他費用</summary>	
        public const string other_cost = "other_cost";
        /// <summary>面取り費用</summary>	
        public const string chamfer_cost = "chamfer_cost";
        /// <summary>その他加工費</summary>
        public const string other_processing_cost = "other_processing_cost";
        /// <summary>材料費計</summary>
        public const string material_cost_total = "material_cost_total";
        /// <summary>単価タイプ</summary>
        public const string unit_type = "unit_type";
        /// <summary>単価タイプ名</summary>
        public const string unit_type_name = "unit_type_name";
        /// <summary>単価</summary>
        public const string unit = "unit";
        /// <summary>税区分</summary>
        public const string tax_type = "tax_type";
        /// <summary>端数処理タイプ</summary>
        public const string round_type = "round_type";
        /// <summary>端数処理タイプ名</summary>
        public const string round_type_name = "round_type_name";
        /// <summary>上限数</summary>
        public const string max_qty = "max_qty";
        /// <summary>割引タイプ</summary>
        public const string discount_type = "discount_type";
        /// <summary>割引</summary>
        public const string discount = "discount";
        /// <summary>割引単位タイプ</summary>
        public const string discount_unit_type = "discount_unit_type";
        /// <summary>割引単位タイプ名</summary>
        public const string discount_unit_type_name = "discount_unit_type_name";
        /// <summary>(材料)仕入先コード</summary>
        public const string item_ext3 = "item_ext3";
        /// <summary>(材料)その他加工費</summary>
        public const string item_ext4 = "item_ext4";
        /// <summary>(材料)材料費計</summary>
        public const string item_ext5 = "item_ext5";
        /// <summary>(品目)材料数量</summary>
        public const string item_ext6 = "item_ext6";
        /// <summary>(材料)Kg単価(指値)</summary>
        public const string item_ext7 = "item_ext7";

        #endregion DataDictionary

        /// <summary>グループ作業ステータス</summary>
        #region GroupWorkStatus
        /// <summary>全作業開始前</summary>
        public const string GroupWorkStatusNPRC = "NPRC";
        /// <summary>製造中</summary>
        public const string GroupWorkStatusINPRC = "INPRC";
        /// <summary>全作業完了</summary>
        public const string GroupWorkStatusFIN = "FIN";
        #endregion WorkStatus

        /// <summary>メッセージID</summary>
        #region MessageId

        /// <summary>参照不可</summary>
        public const string AUTH_NONE = "AUTH_NONE";
        /// <summary>参照のみ</summary>
        public const string AUTH_READ_ONLY = "AUTH_READ_ONLY";
        /// <summary>参照更新可能</summary>
        public const string AUTH_READ_WRITE = "AUTH_READ_WRITE";
        /// <summary>閉じる</summary>
        public const string CTL_CLOSE = "CTL_CLOSE";
        /// <summary>製造終了日時</summary>
        public const string CTL_MANU_END = "CTL_MANU_END";
        /// <summary>製造開始日時</summary>
        public const string CTL_MANU_START = "CTL_MANU_START";
        /// <summary>計画メモ照会</summary>
        public const string CTL_PLAN_MEMO_READ_ONLY = "CTL_PLAN_MEMO_READ_ONLY";
        /// <summary>主資源タスク</summary>
        public const string CTL_PRIMARY_TASK = "CTL_PRIMARY_TASK";
        /// <summary>前段取り終了日時</summary>
        public const string CTL_SETUP_END = "CTL_SETUP_END";
        /// <summary>前段取り開始日時</summary>
        public const string CTL_SETUP_START = "CTL_SETUP_START";
        /// <summary>後段取り終了日時</summary>
        public const string CTL_TEARDOWN_END = "CTL_TEARDOWN_END";
        /// <summary>後段取り開始日時</summary>
        public const string CTL_TEARDOWN_START = "CTL_TEARDOWN_START";
        /// <summary>有り</summary>
        public const string FLAG_EXIST = "FLAG_EXIST";
        /// <summary>なし</summary>
        public const string FLAG_NONE = "FLAG_NONE";
        /// <summary>＜全て＞</summary>
        public const string LIST_ITEM_ALL = "LIST_ITEM_ALL";
        /// <summary>＜選択してください＞</summary>
        public const string LIST_ITEM_SELECT_PROMPT = "LIST_ITEM_SELECT_PROMPT";
        /// <summary>削除しますが、よろしいですか？</summary>
        public const string MSG_CONFIRM_DELETE = "MSG_CONFIRM_DELETE";
        /// <summary>削除しました。</summary>
        public const string MSG_DELETE = "MSG_DELETE";
        /// <summary>添付ファイルが存在しません。</summary>
        public const string MSG_FILE_NOT_FOUND = "MSG_FILE_NOT_FOUND";
        /// <summary>添付ファイル1のファイルが存在しません。</summary>
        public const string MSG_FILE_NOT_FOUND1 = "MSG_FILE_NOT_FOUND1";
        /// <summary>添付ファイル2のファイルが存在しません。</summary>
        public const string MSG_FILE_NOT_FOUND2 = "MSG_FILE_NOT_FOUND2";
        /// <summary>添付ファイル3のファイルが存在しません。</summary>
        public const string MSG_FILE_NOT_FOUND3 = "MSG_FILE_NOT_FOUND3";
        /// <summary>保存したファイルを開きますか？</summary>
        public const string MSG_FILE_OPEN = "MSG_FILE_OPEN";
        /// <summary>アップロードするファイルを選択してください。</summary>
        public const string MSG_FILE_SELECT = "MSG_FILE_SELECT";
        /// <summary>カテゴリを削除しますか？</summary>
        public const string MSG_KLG_CATEGORY_DELETE = "MSG_KLG_CATEGORY_DELETE";
        /// <summary>{0}さんが管理者メニューを利用中です。</summary>
        public const string MSG_LOCKED_ADMIN_MENU = "MSG_LOCKED_ADMIN_MENU";
        /// <summary>ログインに失敗しました。 ログインID、またはパスワードが異なります。</summary>
        public const string MSG_LOGIN_FAILED = "MSG_LOGIN_FAILED";
        /// <summary>添付ファイルのサイズの合計が、30MBを超えています。</summary>
        public const string MSG_MAX_FILESIZE_OVER = "MSG_MAX_FILESIZE_OVER";
        /// <summary>検索結果が{0}件を超えているため結果を表示できません。検索条件を変更してください。</summary>
        public const string MSG_MAX_ROW = "MSG_MAX_ROW";
        /// <summary>パスワードを更新できませんでした。入力データをもう一度ご確認ください。</summary>
        public const string MSG_PASSWORD_UPDATE_ERROR = "MSG_PASSWORD_UPDATE_ERROR";
        /// <summary>{0}は、少なくとも１つチェックを入れる必要があります。</summary>
        public const string MSG_CHECK_AT_LEAST_1_ERROR = "MSG_CHECK_AT_LEAST_1_ERROR";
        /// <summary>多重起動できません。</summary>
        public const string MSG_MULTIPLE_START_ERROR = "MSG_MULTIPLE_START_ERROR";
        /// <summary>{0}は、必須項目です。</summary>
        public const string MSG_REQUIRED_ITEM = "MSG_REQUIRED_ITEM";
        /// <summary>全完了時は、必須です。</summary>
        public const string MSG_REQUIRED_WITH_FINISH = "MSG_REQUIRED_WITH_FINISH";
        /// <summary>添付ファイル{0}と添付ファイル{1}のファイル名が同じです。</summary>
        public const string MSG_SAME_FILE_NAME_ERROR = "MSG_SAME_FILE_NAME_ERROR";
        /// <summary>同名のノードが既にあります。</summary>
        public const string MSG_SAME_NODE_ERROR = "MSG_SAME_NODE_ERROR";
        /// <summary>{0}が{1}より後になっています。</summary>
        public const string MSG_TIMELAG_ERROR = "MSG_TIMELAG_ERROR";
        /// <summary>サポートされていないメディアフォーマットです。</summary>
        public const string MSG_UNSUPPORTED_MEDIA_TYPE = "MSG_UNSUPPORTED_MEDIA_TYPE";
        /// <summary>編集中</summary>
        public const string MST_EDITING = "MST_EDITING";
        /// <summary>新しいカテゴリ</summary>
        public const string NEW_CATEGORY = "NEW_CATEGORY";
        /// <summary>開始前</summary>
        public const string PROGRESS_STATUS_PRE = "PROGRESS_STATUS_PRE";
        /// <summary>着手済</summary>
        public const string PROGRESS_STATUS_S = "PROGRESS_STATUS_S";
        /// <summary>着手遅れ</summary>
        public const string PROGRESS_STATUS_SDLY = "PROGRESS_STATUS_SDLY";
        /// <summary>完了遅れ</summary>
        public const string PROGRESS_STATUS_FDLY = "PROGRESS_STATUS_FDLY";
        /// <summary>完了済</summary>
        public const string PROGRESS_STATUS_FIN = "PROGRESS_STATUS_FIN";
        /// <summary>開始、終了時刻を入力してください。</summary>
        public const string MSG_INPUT_START_AND_END = "MSG_INPUT_START_AND_END";
        /// <summary>紐づいている実績メモ、不良情報も削除されますが、よろしいですか？</summary>
        public const string MSG_CONFIRM_RESULT_DELETE = "MSG_CONFIRM_RESULT_DELETE";
        /// <summary>数値を入力してください。</summary>
        public const string MSG_INPUT_DECIMAL = "MSG_INPUT_DECIMAL";
        /// <summary>以下のエラーがあるため、データを取込むことができません。 {0} FLEXSCHEを起動してエラーを修正しますか？ 「いいえ」を選択した場合、データは取込みません。</summary>
        public const string MSG_IMPORT_ERROR = "MSG_IMPORT_ERROR";
        /// <summary>取込対象</summary>
        public const string MSG_IMPORT_TARGET = "MSG_IMPORT_TARGET";
        /// <summary>{0}は、{1}文字以内で入力してください。</summary>
        public const string MSG_LENGTH_OVER = "MSG_LENGTH_OVER";
        /// <summary>パート</summary>
        public const string CTL_PART = "CTL_PART";
        /// <summary>前段取り</summary>
        public const string CTL_SETUP = "CTL_SETUP";
        /// <summary>製造</summary>
        public const string CTL_MANU = "CTL_MANU";
        /// <summary>後段取り</summary>
        public const string CTL_TEARDOWN = "CTL_TEARDOWN";
        /// <summary>間接作業</summary>
        public const string CTL_INDIRECT = "CTL_INDIRECT";
        /// <summary>開始日時(計画)</summary>
        public const string CTL_PLAN_START = "CTL_PLAN_START";
        /// <summary>終了日時(計画)</summary>
        public const string CTL_PLAN_END = "CTL_PLAN_END";
        /// <summary>開始日時(実績)</summary>
        public const string CTL_RESULT_START = "CTL_RESULT_START";
        /// <summary>終了日時(実績)</summary>
        public const string CTL_RESULT_END = "CTL_RESULT_END";
        /// <summary>計画時間</summary>
        public const string CTL_PLAN_MINUTES = "CTL_PLAN_MINUTES";
        /// <summary>実績時間</summary>
        public const string CTL_RESULT_MINUTES = "CTL_RESULT_MINUTES";
        /// <summary>予実差</summary>
        public const string CTL_DIFF_MINUTES = "CTL_DIFF_MINUTES";
        /// <summary>数量</summary>
        public const string CTL_OUT_QTY = "CTL_OUT_QTY";
        /// <summary>実績数量</summary>
        public const string CTL_RESULT_QTY = "CTL_RESULT_QTY";
        /// <summary>不良数量</summary>
        public const string CTL_FAILURE_QTY = "CTL_FAILURE_QTY";
        /// <summary>計画</summary>
        public const string CTL_PLAN = "CTL_PLAN";
        /// <summary>実績</summary>
        public const string CTL_RESULT = "CTL_RESULT";
        /// <summary>時間</summary>
        public const string CTL_MINUTES = "CTL_MINUTES";
        /// <summary>区分</summary>
        public const string CTL_DIV = "CTL_DIV";
        /// <summary>不良原因へドリルダウン(&F)</summary>
        public const string CMD_DD_CAUSE = "CMD_DD_CAUSE";
        /// <summary>不良内容へドリルダウン(&F)</summary>
        public const string CMD_DD_CONTENT = "CMD_DD_CONTENT";
        /// <summary>自動更新なし</summary>
        public const string CMD_NON_UPDATE = "CMD_NON_UPDATE";
        /// <summary>稼働状況(&M)</summary>
        public const string CMD_RUNNING_STATE = "CMD_RUNNING_STATE";
        /// <summary>更新間隔(&I)</summary>
        public const string CMD_UPDATE_INT = "CMD_UPDATE_INT";
        /// <summary>不良情報参照(&F)</summary>
        public const string CMD_FAIL_INFO = "CMD_FAIL_INFO";
        /// <summary>資源別作業指示(&L)</summary>
        public const string CMD_RES_OPE_INST = "CMD_RES_OPE_INST";
        /// <summary>オーダー別作業指示(&O)</summary>
        public const string CMD_ORD_OPE_INST = "CMD_ORD_OPE_INST";
        /// <summary>最新表示(&N)</summary>
        public const string CMD_NEW = "CMD_NEW";
        /// <summary>設定(&C)</summary>
        public const string CMD_CONF = "CMD_CONF";
        /// <summary>全体</summary>
        public const string P_ALL = "P_ALL";
        /// <summary>年月</summary>
        public const string P_YEAR_MONTH = "P_YEAR_MONTH";
        /// <summary>不良率(%)</summary>
        public const string P_FAIL_RATE = "P_FAIL_RATE";
        /// <summary>遵守率(%)</summary>
        public const string P_IMP_RATE = "P_IMP_RATE";
        /// <summary>平均実績時間(h)</summary>
        public const string P_LEAD_TIME = "P_LEAD_TIME";
        /// <summary>作業時間(h)</summary>
        public const string P_OPE_TIME = "P_OPE_TIME";
        /// <summary>不良数量(個)</summary>
        public const string P_FAIL_NUM = "P_FAIL_NUM";
        /// <summary>累積比率(%)</summary>
        public const string P_ACC_RATIO = "P_ACC_RATIO";
        /// <summary>不良内容</summary>
        public const string P_FAIL_CONTENT = "P_FAIL_CONTENT";
        /// <summary>不良原因</summary>
        public const string P_FAIL_CAUSE = "P_FAIL_CAUSE";
        /// <summary>なし</summary>
        public const string LBL_NON = "LBL_NON";
        /// <summary>直近</summary>
        public const string LBL_RECENT = "LBL_RECENT";
        /// <summary>日分</summary>
        public const string LBL_DAY = "LBL_DAY";
        /// <summary>時点</summary>
        public const string LBL_POINT = "LBL_POINT";
        /// <summary>ヶ月</summary>
        public const string LBL_MONTH = "LBL_MONTH";
        /// <summary>自動更新間隔</summary>
        public const string LBL_AUTO_UPD_INT = "LBL_AUTO_UPD_INT";
        /// <summary>時間</summary>
        public const string LBL_HOUR = "LBL_HOUR";
        /// <summary>該当件数</summary>
        public const string LBL_HITS_NUM = "LBL_HITS_NUM";
        /// <summary>件</summary>
        public const string LBL_HITS = "LBL_HITS";
        /// <summary>分</summary>
        public const string LBL_MINUTES = "LBL_MINUTES";
        /// <summary>パネル設定</summary>
        public const string P_CTL_PANEL_CONF = "P_CTL_PANEL_CONF";
        /// <summary>ルート</summary>
        public const string P_CTL_ROOT = "P_CTL_ROOT";
        /// <summary>新しいグループ</summary>
        public const string P_CTL_NEW_GRP = "P_CTL_NEW_GRP";
        /// <summary>原因パレート図</summary>
        public const string P_CTL_CAUSE_PARETO = "P_CTL_CAUSE_PARETO";
        /// <summary>不良原因パレート図</summary>
        public const string P_CTL_F_CAUSE_PARETO = "P_CTL_F_CAUSE_PARETO";
        /// <summary>内容パレート図</summary>
        public const string P_CTL_CONTENT_PARETO = "P_CTL_CONTENT_PARETO";
        /// <summary>不良内容パレート図</summary>
        public const string P_CTL_F_CONTENT_PARETO = "P_CTL_F_CONTENT_PARETO";
        /// <summary>表示グループ数が上限に達しました。</summary>
        public const string P_MSG_GRP_MAX = "P_MSG_GRP_MAX";
        /// <summary>同じグループ名は付けられません。</summary>
        public const string P_MSG_SAME_GRP = "P_MSG_SAME_GRP";
        /// <summary>設定が保存されていませんが、切り替えますか？</summary>
        public const string P_MSG_NO_SAVE = "P_MSG_NO_SAVE";
        /// <summary>表示項目数が上限に達しました。</summary>
        public const string P_MSG_ITEM_MAX = "P_MSG_ITEM_MAX";
        /// <summary>{0}のパネルIDが重複しています。</summary>
        public const string MSG_DUPLICATE_PANEL = "MSG_DUPLICATE_PANEL";
        /// <summary>その他</summary>
        public const string P_OTHER = "P_OTHER";
        /// <summary>未入力</summary>
        public const string P_NOT_ENTERED = "P_NOT_ENTERED";
        /// <summary>参照権限がありません。</summary>
        public const string P_NON_PERMISSION = "P_NON_PERMISSION";
        /// <summary>編集中はソートできません。</summary>
        public const string MSG_CANNOT_SORT = "MSG_CANNOT_SORT";
        /// <summary>前回入力時間より前の時間になっています。</summary>
        public const string MSG_TIME_PAST_ERROR = "MSG_TIME_PAST_ERROR";
        /// <summary>権限：システム管理者</summary>
        public const string AUTH_TYPE_ADMIN = "AUTH_TYPE_ADMIN";
        /// <summary>権限：計画者</summary>
        public const string AUTH_TYPE_PLANNER = "AUTH_TYPE_PLANNER";
        /// <summary>権限：作業者</summary>
        public const string AUTH_TYPE_WORKER = "AUTH_TYPE_WORKER";
        /// <summary>エラー通知を消去</summary>
        public const string P_CTL_ERROR_BTN = "P_CTL_ERROR_BTN";
        /// <summary>：「{0}」は削除されたので集計対象外です。集計対象を設定してください。</summary>
        public const string P_MSG_ITEM_DEL = "P_MSG_ITEM_DEL";
        /// <summary>：選択されていた「{0}」は削除されたのでデフォルト設定に変更されました。</summary>
        public const string P_MSG_RES_DEL = "P_MSG_RES_DEL";
        /// <summary>明細開始日時</summary>
        public const string CTL_DETAIL_START = "CTL_DETAIL_START";
        /// <summary>明細終了日時</summary>
        public const string CTL_DETAIL_END = "CTL_DETAIL_END";
        /// <summary>明細時間</summary>
        public const string CTL_DETAIL_MINUTES = "CTL_DETAIL_MINUTES";
        /// <summary>コンテンツ照会</summary>
        public const string CTL_CONTENTS_READ_ONLY = "CTL_CONTENTS_READ_ONLY";
        /// <summary>{0}.所属グループに複数項目セットすることはできません。</summary>
        public const string MSG_DUPLICATE_PARENT = "MSG_DUPLICATE_PARENT";
        /// <summary>品目マスタ</summary>
        public const string TBL_ITEM = "TBL_ITEM";
        /// <summary>資源マスタ</summary>
        public const string TBL_RESOURCE = "TBL_RESOURCE";
        /// <summary>猶予</summary>
        public const string P_CTL_POSTPONEMENT = "P_CTL_POSTPONEMENT";
        /// <summary>直近</summary>
        public const string P_CTL_LASTEST = "P_CTL_LASTEST";
        /// <summary>インデックスファイルが存在しないか、壊れています。 インデックスを再構築してください。</summary>
        public const string MSG_INDEX_ERROR = "MSG_INDEX_ERROR";
        /// <summary>（仕掛中）</summary>
        public const string MSG_INPRC = "MSG_INPRC";
        /// <summary>削除権限がありません。</summary>
        public const string MSG_NO_DELETE_PERMISSION = "MSG_NO_DELETE_PERMISSION";
        /// <summary>{0}が重複しています。</summary>
        public const string MSG_DUPLICATE = "MSG_DUPLICATE";
        /// <summary>「{0}」は、システムで利用するため設定できません。</summary>
        public const string MSG_RESERVED = "MSG_RESERVED";
        /// <summary>未選択</summary>
        public const string MSG_NOT_SELECTED = "MSG_NOT_SELECTED";
        /// <summary>未入力</summary>
        public const string MSG_NOT_ENTRY = "MSG_NOT_ENTRY";
        /// <summary>添付ファイルは、別のユーザによって削除されたため表示できません。</summary>
        public const string MSG_ALREADY_DELETED = "MSG_ALREADY_DELETED";
        /// <summary>削除グループ</summary>
        public const string DELETE_GROUP_NAME = "DELETE_GROUP_NAME";
        /// <summary>新規追加中に、計画メモを編集することはできません。 一度、オーダーを追加更新してから編集してください。</summary>
        public const string MSG_CANNOT_EDIT_PLAN_MEMO = "MSG_CANNOT_EDIT_PLAN_MEMO";
        /// <summary>別のユーザによって対象カテゴリが削除されたため、更新できません。</summary>
        public const string MSG_CATEGORY_ALREADY_DELETED = "MSG_CATEGORY_ALREADY_DELETED";
        /// <summary>作業実績が編集されていますが、保存せずに閉じますか？</summary>
        public const string MSG_RESULT_NO_SAVE = "MSG_RESULT_NO_SAVE";
        /// <summary>書込み権限がないため、移動できません。</summary>
        public const string MSG_NO_MOVE_PERMISSION = "MSG_NO_MOVE_PERMISSION";
        /// <summary>不良情報照会</summary>
        public const string CTL_FAILURE_INFO_READ_ONLY = "CTL_FAILURE_INFO_READ_ONLY";
        /// <summary>日以下</summary>
        public const string LBL_DAYS_OR_LESS = "LBL_DAYS_OR_LESS";
        /// <summary>{0}を算出することができません。入力内容を見直してください。</summary>
        public const string MSG_CANNOT_CALCULATE_ERROR = "MSG_CANNOT_CALCULATE_ERROR";
        /// <summary>バーコードが登録にありません。</summary>
        public const string MSG_NOT_EXISTS_BARCODE = "MSG_NOT_EXISTS_BARCODE";
        /// <summary>作業時間は終了時に指定してください</summary>
        public const string MSG_WORKTIME_AT_END = "MSG_WORKTIME_AT_END";
        /// <summary>{0}にマイナスは入力できません。</summary>
        public const string MSG_NOT_INPUT_MINUS = "MSG_NOT_INPUT_MINUS";
        /// <summary>{0}が変更されております。</summary>
        public const string MSG_ALREADY_CHANGED = "MSG_ALREADY_CHANGED";
        /// <summary>もう一度{0}を指定してください。</summary>
        public const string MSG_INPUT_ONCE_AGAIN = "MSG_INPUT_ONCE_AGAIN";
        /// <summary>バーコード指定後に画面操作が行われました。もう一度入力してください。</summary>
        public const string MSG_CHANGED_AFTER_BARCODE = "MSG_CHANGED_AFTER_BARCODE";
        /// <summary>バーコードからの入力をキャンセルしました</summary>
        public const string MSG_CANCEL_BARCODE = "MSG_CANCEL_BARCODE";
        /// <summary>選択内容</summary>
        public const string MSG_SELECTED = "MSG_SELECTED";
        /// <summary>種類</summary>
        public const string P_RELATION_TYPE_NAME = "P_RELATION_TYPE_NAME";
        /// <summary>FLEXSCHEプロジェクトをバックアップ中 ...</summary>
        public const string MSG_FS_BACKUP = "MSG_FS_BACKUP";
        /// <summary>{0}は、上から順に設定してください。</summary>
        public const string MSG_INVALID_SEQUENCE_ERROR = "MSG_INVALID_SEQUENCE_ERROR";
        /// <summary>数値(分)で入力してください。</summary>
        public const string MSG_INPUT_MINUTES = "MSG_INPUT_MINUTES";
        /// <summary>{0}が編集中なので、参照モードで開きます。</summary>
        public const string MSG_EDITING_OTHER = "MSG_EDITING_OTHER";
        /// <summary>有効期間開始日時</summary>
        public const string CTL_VALID_START = "CTL_VALID_START";
        /// <summary>有効期間終了日時</summary>
        public const string CTL_VALID_END = "CTL_VALID_END";
        /// <summary>メイン(OKUMA系)</summary>
        public const string CTL_NCPGNO1_TITLE = "CTL_NCPGNO1_TITLE";
        /// <summary>33MC</summary>
        public const string CTL_NCPGNO2_TITLE = "CTL_NCPGNO2_TITLE";
        /// <summary>上位{0}件まで表示</summary>
        public const string CTL_TOP_ROW_LIMIT = "CTL_TOP_ROW_LIMIT";
        /// <summary>プログラムNoは、NC工程にのみ設定できます。</summary>
        public const string MSG_PGNO_NC_ONLY = "MSG_PGNO_NC_ONLY";
        /// <summary>33MCは頭'O'から記述してください。</summary>
        public const string MSG_33MC_PGNO_FORMAT_ERROR = "MSG_33MC_PGNO_FORMAT_ERROR";
        /// <summary>頭'O'から始まっています。</summary>
        public const string MSG_OKUMA_PGNO_FORMAT_ERROR = "MSG_OKUMA_PGNO_FORMAT_ERROR";
        /// <summary>キーワード</summary>
        public const string CTL_KEYWORD = "CTL_KEYWORD";
        /// <summary>{0}を選択してください。</summary>
        public const string MSG_PLEASE_SELECT = "MSG_PLEASE_SELECT";
        /// <summary>取込が完了しました。</summary>
        public const string MSG_IMPORT_COMPLETED = "MSG_IMPORT_COMPLETED";
        /// <summary>図面を編集するには、一度、品目情報を登録しておく必要があります。 登録しますか？</summary>
        public const string MSG_CONFIRM_ITEM_REGIST = "MSG_CONFIRM_ITEM_REGIST";
        /// <summary>図面フォルダパスが間違っています。</summary>
        public const string MSG_DIAGRAM_PATH_ERROR = "MSG_DIAGRAM_PATH_ERROR";
        /// <summary>{0}は、既に開かれています。 処理を中断します。</summary>
        public const string MSG_ALREADY_OPEN_AND_ABORT = "MSG_ALREADY_OPEN_AND_ABORT";
        /// <summary>{0}は、{1}のため出力できません。</summary>
        public const string MSG_CANNOT_OUTPUT = "MSG_CANNOT_OUTPUT";
        /// <summary>既に開かれている</summary>
        public const string MSG_ALREADY_OPEN = "MSG_ALREADY_OPEN";
        /// <summary>未計画状態</summary>
        public const string MSG_STATUS_NPLN = "MSG_STATUS_NPLN";
        /// <summary>未割付状態</summary>
        public const string MSG_STATUS_NASN = "MSG_STATUS_NASN";
        /// <summary>保留状態</summary>
        public const string MSG_STATUS_PND = "MSG_STATUS_PND";
        /// <summary>該当データが見つかりません。 最新のバーコードを読み込んでいるか確認して下さい。</summary>
        public const string MSG_BARCODE_DATA_NOT_FOUND = "MSG_BARCODE_DATA_NOT_FOUND";
        /// <summary>フォルダを選択してください。</summary>
        public const string MSG_DIR_SELECT = "MSG_DIR_SELECT";
        /// <summary>フォルダが見つかりません。再度指定してください。</summary>
        public const string MSG_DIR_NOT_FOUND = "MSG_DIR_NOT_FOUND";
        /// <summary>以下のエラーがあるため、データを取込むことができません。{0}</summary>
        public const string MSG_IMPORT_FILE_ERROR = "MSG_IMPORT_FILE_ERROR";
        /// <summary>{0}に合致するファイルが指定されていないか、存在しません。</summary>
        public const string MSG_IMPORT_FILE_NOT_FOUND = "MSG_IMPORT_FILE_NOT_FOUND";
        /// <summary>プログラムNo.に紐づかない工具データは削除されます。一括取込しますか？</summary>
        public const string MSG_CONFIRM_TOOL_BULK_IMPORT = "MSG_CONFIRM_TOOL_BULK_IMPORT";
        /// <summary>この作業に{0}を入力することはできません。もう一度指定してください。</summary>
        public const string MSG_CANNOT_RESULT_INPUT = "MSG_CANNOT_RESULT_INPUT";
        /// <summary>{0}は既にに完了しているため更新しませんでした。</summary>
        public const string MSG_ALREADY_FIN_NO_UPDATE = "MSG_ALREADY_FIN_NO_UPDATE";
        /// <summary>選択</summary>
        public const string CTL_SELECT = "CTL_SELECT";
        /// <summary>仕入先</summary>
        public const string CTL_SUPPLIER = "CTL_SUPPLIER";
        /// <summary>管理</summary>
        public const string CTL_MANAGEMENT = "CTL_MANAGEMENT";
        /// <summary>{0}件を超えるため、{1}できません。</summary>
        public const string MSG_CANNOT_OVER_LIMIT = "MSG_CANNOT_OVER_LIMIT";
        /// <summary>参照</summary>
        public const string CTL_READONLY = "CTL_READONLY";
        /// <summary>別のユーザによって編集中です。{0}しますか？</summary>
        public const string MSG_CONFIRM_EDITING_OTHER = "MSG_CONFIRM_EDITING_OTHER";
        /// <summary>{0}は、{1}ため出力できません。</summary>
        public const string MSG_CANNOT_OUTPUT_CAUSE = "MSG_CANNOT_OUTPUT_CAUSE";
        /// <summary>ファイル名に利用できない文字が設定されている</summary>
        public const string MSG_FILE_NAME_INVALID_ERR = "MSG_FILE_NAME_INVALID_ERR";
        /// <summary>該当データなし</summary>
        public const string MSG_NO_TARGET_DATA = "MSG_NO_TARGET_DATA";
        /// <summary>添付ファイルが編集中のため、{0}することができません。編集しているアプリケーションを終了してください。</summary>
        public const string MSG_LOCKED_ATTACHMENT_FILE = "MSG_LOCKED_ATTACHMENT_FILE";
        /// <summary>編集</summary>
        public const string CTL_EDIT = "CTL_EDIT";
        /// <summary>カメラにアクセスできませんでした。</summary>
        public const string MSG_CAMERA_NOT_ACCESS = "MSG_CAMERA_NOT_ACCESS";
        /// <summary>読み込み成功</summary>
        public const string MSG_QRCODE_READ_SUCCESS = "MSG_QRCODE_READ_SUCCESS";
        /// <summary>前段取り</summary>
        public const string CTL_UNPLANNED_SETUP = "CTL_UNPLANNED_SETUP";
        /// <summary>製造（計画外）</summary>
        public const string CTL_UNPLANNED_MANU = "CTL_UNPLANNED_MANU";
        /// <summary>後段取り（計画外）</summary>
        public const string CTL_UNPLANNED_TEARDOWN = "CTL_UNPLANNED_TEARDOWN";
        /// <summary>「/&\<>,;"'」は禁止されている文字です。 全角文字など他の文字に置き換えて下さい。</summary>
        public const string MSG_NG_WORD_ERROR = "MSG_NG_WORD_ERROR";
        /// <summary>先頭の「#@%-!=」は禁止されている文字です。全角文字など他の文字に置き換えて下さい。</summary>
        public const string MSG_TOP_NG_WORD_ERROR = "MSG_TOP_NG_WORD_ERROR";
        /// <summary>オーダー品目</summary>
        public const string CTL_ORDER_ITEM = "CTL_ORDER_ITEM";
        /// <summary>グループを選択してください</summary>
        public const string MSG_NEED_SELECT_GROUP = "MSG_NEED_SELECT_GROUP";
        /// <summary>作業を選択してください</summary>
        public const string MSG_NEED_SELECT_OPERATION = "MSG_NEED_SELECT_OPERATION";
        /// <summary>作業明細がグループごとのものに上書きされますがよろしいですか？</summary>
        public const string MSG_CONFIRM_RESULT_DETAILS = "MSG_CONFIRM_RESULT_DETAILS";
        /// <summary>グループが削除されますが、よろしいですか？</summary>
        public const string MSG_CONFIRM_DELETE_GROUP = "MSG_CONFIRM_DELETE_GROUP";
        /// <summary>選択作業の元のステータスが変更されています。</summary>
        public const string MSG_CHANGED_AFTER_LOAD = "MSG_CHANGED_AFTER_LOAD";
        /// <summary>グループ名を必ず入力してください。</summary>
        public const string MSG_NEED_GROUP_NAME = "MSG_NEED_GROUP_NAME";
        /// <summary>カテゴリを選択してください。</summary>
        public const string MSG_NOT_SELECT_CATEGORY = "MSG_NOT_SELECT_CATEGORY";
        /// <summary>実績資源を登録してください。</summary>
        public const string MSG_NOT_EXISTS_RESOURCE = "MSG_NOT_EXISTS_RESOURCE";
        /// <summary>以下の作業は別ユーザが編集中のため、強制完了できませんでした。</summary>
        public const string MSG_NOT_FORCE_COMPLETE = "MSG_NOT_FORCE_COMPLETE";
        /// <summary>以下の作業は別ユーザが編集中のため、グループに登録できません。</summary>
        public const string MSG_NOT_ADD_EDITING_OPE = "MSG_NOT_ADD_EDITING_OPE";
        /// <summary>以下のグループは別ユーザが編集中のため、削除できません。</summary>
        public const string MSG_NOT_DELETE_EDITING_GROUP = "MSG_NOT_DELETE_EDITING_GROUP";
        /// <summary>{0}が編集中なので、{1}に作業を追加できません。</summary>
        public const string MSG_NOT_ADD_EDITING_GROUP = "MSG_NOT_ADD_EDITING_GROUP";
        /// <summary>以下の作業は既に他のグループに属しているため、グループに追加できません。</summary>
        public const string MSG_DUPLICATE_GROUP = "MSG_DUPLICATE_GROUP";
        /// <summary>以下の作業は既に追加対象グループに属しているため、グループに追加できません。</summary>
        public const string MSG_DUPLICATE_THIS_GROUP = "MSG_DUPLICATE_THIS_GROUP";
        /// <summary>以下の作業は計画外作業のため、グループに追加できません。</summary>
        public const string MSG_NOT_ADD_UNPLANNED_OPE = "MSG_NOT_ADD_UNPLANNED_OPE";
        /// <summary>工程{0}作業コード</summary>
        public const string CTL_PROC_N_OPE_CD = "CTL_PROC_N_OPE_CD";
        /// <summary>計画外{0}作業コード</summary>
        public const string CTL_UNPLANNED_N_OPE_CD = "CTL_UNPLANNED_N_OPE_CD";
        /// <summary>社内注文に含まれているため削除できません。</summary>
        public const string MSG_NOT_DEL_EXISTS_SALES_ORDER = "MSG_NOT_DEL_EXISTS_SALES_ORDER";
        /// <summary>{0}が編集されているため、保存してから出力します。</summary>
        public const string MSG_PRINT_BEFORE_SAVE = "MSG_PRINT_BEFORE_SAVE";
        /// <summary>{0}が編集されていますが、保存せずに閉じますか？</summary>
        public const string MSG_NO_SAVE = "MSG_NO_SAVE";
        /// <summary>一覧</summary>
        public const string CTL_LIST = "CTL_LIST";
        /// <summary>別のユーザによって更新されたため、対象在庫が存在しません。最新の現在庫を確認してください。</summary>
        public const string MSG_STOCK_ZERO = "MSG_STOCK_ZERO";
        /// <summary>別のユーザによって更新されたため、入庫場所が存在しません。最新の現在庫を確認してください。</summary>
        public const string MSG_LOCATION_NONE = "MSG_LOCATION_NONE";
        /// <summary>{0}さんが場所マスタ管理を利用中です。</summary>
        public const string MSG_LOCKED_LOCATION = "MSG_LOCKED_LOCATION";
        /// <summary>別ユーザが編集中ですが、{0}しますか？ {1}</summary>
        public const string MSG_PRINT_EVEN_THOUGH_LOCK = "MSG_PRINT_EVEN_THOUGH_LOCK";
        /// <summary>印刷</summary>
        public const string CTL_PRINT = "CTL_PRINT";
        /// <summary>別のユーザーにより削除された</summary>
        public const string MSG_DELETED_BY_ANOTHER_USER = "MSG_DELETED_BY_ANOTHER_USER";
        /// <summary>既に入出庫登録を行っています。修正する場合は、在庫管理メニューから入出庫(計画外)として数量補正して下さい。</summary>
        public const string MSG_ALREADY_INVENTORY_INPUT = "MSG_ALREADY_INVENTORY_INPUT";
        /// <summary>別のユーザによって削除されました。</summary>
        public const string MSG_DELETED_BY_USER = "MSG_DELETED_BY_USER";
        /// <summary>既に棚卸中のものが含まれています。</summary>
        public const string MSG_ALREADY_INVENTORY_START = "MSG_ALREADY_INVENTORY_START";
        /// <summary>既に棚卸作業が完了しています。</summary>
        public const string MSG_ALREADY_INVENTORY_FINISHED = "MSG_ALREADY_INVENTORY_FINISHED";
        /// <summary>以下の作業は入出庫作業のため、グループに追加できません。 {0}</summary>
        public const string MSG_NOT_ADD_INOUT_OPE = "MSG_NOT_ADD_INOUT_OPE";
        /// <summary>{0}を起動しますか？</summary>
        public const string MSG_CONFIRM_APP_INIT = "MSG_CONFIRM_APP_INIT";
        /// <summary>FLEXSCHE</summary>
        public const string APP_FLEXSCHE = "APP_FLEXSCHE";
        /// <summary>{0}件出力しました。</summary>
        public const string MSG_EXPORTED_ROW = "MSG_EXPORTED_ROW";

        /// <summary>重複項目が存在するため、追加できません。</summary>
        public const string MSG_EXISTS_DUPLICATE = "MSG_EXISTS_DUPLICATE";
        /// <summary>入庫登録</summary>
        public const string TITLE_IN_REGISTRATION = "TITLE_IN_REGISTRATION";
        /// <summary>出庫登録</summary>
        public const string TITLE_OUT_REGISTRATION = "TITLE_OUT_REGISTRATION";
        /// <summary>廃棄登録</summary>
        public const string TITLE_DISCARD_REGISTRATION = "TITLE_DISCARD_REGISTRATION";
        /// <summary>入庫日</summary>
        public const string LBL_IN_DATE = "LBL_IN_DATE";
        /// <summary>出庫日</summary>
        public const string LBL_OUT_DATE = "LBL_OUT_DATE";
        /// <summary>廃棄日</summary>
        public const string LBL_DISCARD_DATE = "LBL_DISCARD_DATE";
        /// <summary>出庫先選択</summary>
        public const string TITLE_OUT_LOCATION_SELECT = "TITLE_OUT_LOCATION_SELECT";
        /// <summary>廃棄先選択</summary>
        public const string TITLE_DISCARD_LOCATION_SELECT = "TITLE_DISCARD_LOCATION_SELECT";
        /// <summary>廃棄先</summary>
        public const string LBL_DISCARD_LOCATION = "LBL_DISCARD_LOCATION";
        /// <summary>場所選択</summary>
        public const string TITLE_LOCATION_SELECT = "TITLE_LOCATION_SELECT";
        /// <summary>出庫</summary>
        public const string CTL_PLANNED_OUT = "CTL_PLANNED_OUT";
        /// <summary>出庫(&O)</summary>
        public const string CTL_PLANNED_OUT_MENU = "CTL_PLANNED_OUT_MENU";
        /// <summary>FLEXSCHEのデータにエラーがあります。詳細は「データ入力チェック」ルールを実行して確認して下さい。</summary>
        public const string MSG_FLEXSCHE_DATA_ERROR = "MSG_FLEXSCHE_DATA_ERROR";
        /// <summary>リンクするファイルを選択してください。</summary>
        public const string MSG_LINK_FILE_SELECT = "MSG_LINK_FILE_SELECT";
        /// <summary>リンクするファイルが存在しません。</summary>
        public const string MSG_LINK_FILE_NOT_FOUND = "MSG_LINK_FILE_NOT_FOUND";
        /// <summary>{0}によって編集中です。{1}しますか？</summary>
        public const string MSG_CONFIRM_EDITING_OTHER_NAME = "MSG_CONFIRM_EDITING_OTHER_NAME";

        #endregion MessageId

        #region BarcodeDelimiter
        /// <summary>バーコードデミリタ</summary>
        public const string BarcodeDelimiter = "$";
        /// <summary>バーコードデミリタ（全角）</summary>
        public const string BarcodeDelimiterZenkaku = "＄";
        #endregion BarcodeDelimiter

        /// <summary>バーコードプリフィックス</summary>
        #region BarcodePrefix
        /// <summary>オーダー"A"</summary>
        public const string BarcodePrefixOrder = "A";
        /// <summary>作業"B"</summary>
        public const string BarcodePrefixOperation = "B";
        /// <summary>資源"C"</summary>
        public const string BarcodePrefixResource = "C";
        /// <summary>簡易作業時間"D"</summary>
        public const string BarcodePrefixSimpleWorkTime = "D";
        /// <summary>棚卸"I"</summary>
        public const string BarcodePrefixPhisicalInventory = "I";
        /// <summary>場所別在庫"S"</summary>
        public const string BarcodePrefixStockByLocation = "S";
        /// <summary>実績入力"Z"</summary>
        public const string BarcodePrefixResultInput = "Z";
        #endregion BarcodePrefix

        /// <summary>バーコード値</summary>
        #region BarcodeData
        /// <summary>実績入力-開始</summary>
        public const string BarcodeDataResultInputStart = "-010";
        /// <summary>実績入力-終了</summary>
        public const string BarcodeDataResultInputEnd = "-020";
        /// <summary>実績入力-確定</summary>
        public const string BarcodeDataResultInputApply = "-030";
        /// <summary>実績入力-入力キャンセル</summary>
        public const string BarcodeDataResultInputCancel = "-040";
        /// <summary>実績入力-作業中断</summary>
        public const string BarcodeDataResultInputInterruption = "-050";
        /// <summary>実績入力-作業再開(バーコード利用なし）</summary>
        public const string BarcodeDataResultInputRestart = "-055";
        /// <summary>//実績入力-1つ前に戻す</summary>
        public const string BarcodeDataResultInputPreviousOne = "-060";
        /// <summary>//実績入力-強制完了</summary>
        public const string BarcodeDataResultInputForceEnd = "-070";

        #endregion BarcodeData

        /// <summary>間接作業ステータス</summary>
        #region IndirectStatus
        /// <summary>間接作業ステータス:なし</summary>
        public const string IndirectStatusN = "N";
        /// <summary>間接作業ステータス:仕掛中</summary>
        public const string IndirectStatusS = "S";
        /// <summary>間接作業ステータス:完了</summary>
        public const string IndirectStatusF = "F";
        #endregion IndirectStatus

        /// <summary>作業状況</summary>
        #region WorkStatus
        /// <summary>未仕掛</summary>
        public const string WorkStatusNPRC = "NPRC";
        /// <summary>仕掛中</summary>
        public const string WorkStatusINPRC = "INPRC";
        /// <summary>完了</summary>
        public const string WorkStatusFIN = "FIN";
        #endregion WorkStatus

        /// <summary>作業区分</summary>
        #region WorkDiv
        /// <summary>間接作業</summary>
        public const string WorkDivIndirect = "I";
        /// <summary>直接作業</summary>
        public const string WorkDivDirect = "D";
        /// <summary>計画外作業</summary>
        public const string WorkDivUnplanned = "U";
        #endregion WorkDiv

        /// <summary>メモ区分</summary>
        #region MemoDiv
        /// <summary>計画メモ</summary>
        public const string MemoDivP = "P";
        /// <summary>実績メモ</summary>
        public const string MemoDivR = "R";
        /// <summary>計画/実績メモ</summary>
        public const string MemoDivPR = "PR";
        #endregion MemoDiv

        /// <summary>削除フラグ</summary>
        #region DeleteFlag
        public const string DeleteFlagDeleted = "1";
        #endregion DeleteFlag

        /// <summary>オーダー作業状況</summary>
        #region OrderWorkStatus
        /// <summary>未計画</summary>
        public const string OrderWorkStatusNPLN = "NPLN";
        /// <summary>未割付</summary>
        public const string OrderWorkStatusNASN = "NASN";
        /// <summary>未仕掛</summary>
        public const string OrderWorkStatusNPRC = "NPRC";
        /// <summary>仕掛中</summary>
        public const string OrderWorkStatusINPRC = "INPRC";
        /// <summary>保留</summary>
        public const string OrderWorkStatusPND = "PND";
        /// <summary>完了</summary>
        public const string OrderWorkStatusFIN = "FIN";
        #endregion OrderWorkStatus

        /// <summary>システム権限</summary>
        #region SystemAuthority
        /// <summary>管理者権限</summary>
        public const string SystemAuthorityAdmin = "1";
        /// <summary>計画者権限</summary>
        public const string SystemAuthorityPlanner = "2";
        /// <summary>作業者権限</summary>
        public const string SystemAuthorityWorker = "3";
        #endregion SystemAuthority

        /// <summary>オーダー種別</summary>
        #region OrderType
        /// <summary>生産オーダー</summary>
        public const string OrderTypeProduction = "P";
        /// <summary>消費オーダー</summary>
        public const string OrderTypeConsumption = "C";
        /// <summary>出荷オーダー</summary>
        public const string OrderTypeShipping = "S";
        /// <summary>入荷オーダー</summary>
        public const string OrderTypeArrival = "A";
        #endregion OrderType

        /// <summary>オーダー新規種別</summary>
        #region OrderFreshness
        /// <summary>既存オーダー</summary>
        public const string OrderFreshnessExisting = "E";
        /// <summary>新規オーダー</summary>
        public const string OrderFreshnessNew = "N";
        /// <summary>更新オーダー</summary>
        public const string OrderFreshnessUpdate = "U";
        #endregion OrderFreshness

        /// <summary>補充種別</summary>
        #region ReplenishType
        /// <summary>補充種別:なし</summary>
        public const string ReplenishTypeNone = "N";
        /// <summary>補充種別:一時補充オーダー</summary>
        public const string ReplenishTypeTemp = "T";
        /// <summary>補充種別:確定補充オーダー</summary>
        public const string ReplenishTypeFix = "F";
        #endregion ReplenishType


        /// <summary>割付け方法</summary>
        #region DispatchMethod
        /// <summary>フォワード</summary>
        public const string DispatchMethodF = "F";
        /// <summary>バックワード</summary>
        public const string DispatchMethodB = "B";
        #endregion DispatchMethod

        /// <summary>簡易作業時間（特別値）</summary>
        #region SimpleWorkTime
        /// <summary>簡易作業時間計画値</summary>
        public const string SimpleWorkTimePlanManu = "0";
        #endregion SimpleWorkTime

        /// <summary>テーブル名</summary>
        #region TableName
        public const string TableNameFtOrder = "ft_order";
        public const string TableNameFtResource = "ft_resource";
        public const string TableNameFtProc = "ft_proc";
        public const string TableNameFtItem = "ft_item";
        public const string TableNameFtResult = "ft_result";
        public const string TableNameFtOperation = "ft_operation";
        public const string TableNameFtMtrlSub = "ft_mtrl_sub";
        public const string TableNameFtOutSub = "ft_out_sub";
        public const string TableNameFtSubtask = "ft_subtask";
        public const string TableNameMClient = "m_client";
        public const string TableNameTOperationBarcode = "t_operation_barcode";
        public const string TableNameTOrderBarcode = "t_order_barcode";
        public const string TableNameTResourceBarcode = "t_resource_barcode";
        public const string TableNameTSimpleWorkTimeBarcode = "t_simpleworktime_barcode";
        public const string TableNameTUnplannedResult = "t_unplanned_result";
        #endregion TableName

        /// <summary>CSVファイル名</summary>
        #region CSVName
        public const string CSVNameOrder = "order.csv";
        public const string CSVNameResource = "resource.csv";
        public const string CSVNameProc = "proc.csv";
        public const string CSVNameItem = "item.csv";
        public const string CSVNameResult = "mes_result.csv";
        public const string CSVNameInventory = "mes_inventory.csv";
        public const string CSVNameOperation = "operation.csv";
        public const string CSVNameFreeCale = "operation.csv";
        public const string CSVNameFlexsheError = "flexsche_error.csv";
        #endregion CSVName

        /// <summary>ダッシュボードレイアウトファイル</summary>
        #region LayoutFile
        public const string DashboardLayoutFile = "DashboardLayout.xml";
        #endregion LayoutFile

        /// <summary>共通設定</summary>
        #region CommonConf
        /// <summary>デバイス認証を行うかどうか。</summary>
        public const string EnableDeviceAuth = "EnableDeviceAuth";
        /// <summary>FLEXSCHEのプロジェクトパスを絶対パスで指定</summary>
        public const string FlexschePath = "FlexschePath";
        /// <summary>実績承認機能を有効とするか。</summary>
        public const string EnableResultApprove = "EnableResultApprove";
        /// <summary>実績一括登録機能を有効とするか。</summary>
        public const string EnableBulkResultEdit = "EnableBulkResultEdit";
        /// <summary>納期猶予警告期間(残りn日で警告表示)</summary>
        public const string DeadlineWarningTerm = "DeadlineWarningTerm";
        /// <summary>検索時の最大取得行数</summary>
        public const string MaxRowCount = "MaxRowCount";
        /// <summary>新規追加行ダイアログ</summary>
        public const string MaxAppendRowCount = "MaxAppendRowCount";
        /// <summary>納期逼迫リストパネルでハイライト表示する日数</summary>
        public const string ImpendingDate = "ImpendingDate";
        /// <summary>30MB</summary>
        public const string MaxContentByte = "MaxContentByte";
        /// <summary>パネルの最大表示グループ数</summary>
        public const string MaxPanelDispGroup = "MaxPanelDispGroup";
        /// <summary>パネルの最大表示項目数</summary>
        public const string MaxPanelDispItem = "MaxPanelDispItem";
        /// <summary>パネルの最大表示期間(日数)</summary>
        public const string MaxPanelDispDays = "MaxPanelDispDays";
        /// <summary>パネルの最大表示期間(月数)</summary>
        public const string MaxPanelDispMonth = "MaxPanelDispMonth";
        /// <summary>パレート図のパネルの最大集計期間</summary>
        public const string MaxParetoDispMonth = "MaxParetoDispMonth";
        /// <summary>完了(LightGray)</summary>
        public const string ColorFIN = "ColorFIN";
        /// <summary>仕掛中(Aquamarine)</summary>
        public const string ColorINPRC = "ColorINPRC";
        /// <summary>未計画(LightCyan)</summary>
        public const string ColorNPLN = "ColorNPLN";
        /// <summary>未仕掛(White)</summary>
        public const string ColorNPRC = "ColorNPRC";
        /// <summary>オーダー完了(LightGray)</summary>
        public const string ColorOdrFIN = "ColorOdrFIN";
        /// <summary>オーダー保留(Salmon)</summary>
        public const string ColorOdrHLD = "ColorOdrHLD";
        /// <summary>オーダー仕掛中(Aquamarine)</summary>
        public const string ColorOdrINPRC = "ColorOdrINPRC";
        /// <summary>オーダー未割付(Plum)</summary>
        public const string ColorOdrNASN = "ColorOdrNASN";
        /// <summary>オーダー未計画(SkyBlue)</summary>
        public const string ColorOdrNPLN = "ColorOdrNPLN";
        /// <summary>オーダー未仕掛(White)</summary>
        public const string ColorOdrNPRC = "ColorOdrNPRC";
        /// <summary>本日作業(MistyRose)</summary>
        public const string ColorTodayOpe = "ColorTodayOpe";
        /// <summary>FLEXSCHE連携用クライアントID</summary>
        public const string ConnectClientId = "ConnectClientId";
        /// <summary>FLEXSCHE連携用パスワード</summary>
        public const string ConnectClientPassword = "ConnectClientPassword";
        /// <summary>セッションタイムアウト時間（分）</summary>
        public const string SessionTimeOutMinutes = "SessionTimeOutMinutes";
        /// <summary>APServerからみたDBServer上のFx用リポジトリパス</summary>
        public const string ApfileRepoContext = "ApfileRepoContext";
        /// <summary>FLEXSCHEプロジェクトのバックアップ保持数</summary>
        public const string RemainingFileCount = "RemainingFileCount";
        /// <summary>Tmpフォルダ内のファイルの保持日数</summary>
        public const string TmpRetentionDay = "TmpRetentionDay";
        /// <summary>テンポラリファイル削除処理スケジュール（HH:mm形式で記述）</summary>
        public const string SweepSchedule = "SweepSchedule";
        /// <summary>インデックス再構築スケジュール（HH:mm形式で記述）</summary>
        public const string RebuildIndexSchedule = "RebuildIndexSchedule";
        /// <summary>パネル表示情報作成スケジュール（HH:mm形式で記述）</summary>
        public const string PanelInfoSchedule = "PanelInfoSchedule";
        /// <summary>FxDBのバックアップスケジュール（HH:mm形式で記述）</summary>
        public const string FxDbBackupSchedule = "FxDbBackupSchedule";
        /// <summary>FxDBの容量チェックスケジュール（HH:mm形式で記述）</summary>
        public const string FxDbVolumeCheckSchedule = "FxDbVolumeCheckSchedule";
        /// <summary>FxDBの容量チェック時の限界容量（単位はMB）（チェックしない場合、0を設定する）</summary>
        public const string FxDbVolumeCheckWarnLimitMB = "FxDbVolumeCheckWarnLimitMB";
        /// <summary>納期逼迫アラートメールを有効化するかどうか。</summary>
        public const string EnableDeadlineAlertMail = "EnableDeadlineAlertMail";
        /// <summary>納期逼迫アラートスケジュール（HH:mm形式で記述）</summary>
        public const string DeadlineImpendingAlertSchedule = "DeadlineImpendingAlertSchedule";
        /// <summary>アラートメール送信用SMTPサーバアドレス</summary>
        public const string SmtpAddress = "SmtpAddress";
        /// <summary>アラートメール送信用SMTPポート</summary>
        public const string SmtpPort = "SmtpPort";
        /// <summary>送信元アラートメールアドレス</summary>
        public const string FromMailAddress = "FromMailAddress";
        /// <summary>補充オーダーの取込を有効とするか</summary>
        public const string EnableReplenishOrderImport = "EnableReplenishOrderImport";
        /// <summary>全文検索ハイライト色</summary>
        public const string ColorCdFtsHighlight = "ColorCdFtsHighlight";
        /// <summary>作業実績入力のモード。'NORMAL':通常、'NORMAL_MANU':通常（製造のみ）、'SIMPLE_STARTEND'：簡易（着完）、'SIMPLE_END'：簡易（完了のみ）</summary>
        public const string ResultInputMode = "ResultInputMode";
        /// <summary>BOM管理機能を有効とするか</summary>
        public const string EnableBOMControl = "EnableBOMControl";
        /// <summary>上位n行で検索結果を表示</summary>
        public const string TopRowLimit = "TopRowLimit";
        /// <summary>グループ名の背景色</summary>
        public const string ColorGroup = "ColorGroup";
        /// <summary>初期表示自動セットしているが未確定の項目色</summary>
        public const string ColorCdUnsettled = "ColorCdUnsettled";
        /// <summary>就業開始時刻（HH:mm形式で記述）</summary>
        public const string WorkOpenTime = "WorkOpenTime";
        /// <summary>就業昼休開始時刻（HH:mm形式で記述）</summary>
        public const string WorkBreakOpenTime = "WorkBreakOpenTime";
        /// <summary>就業昼休終了時刻（HH:mm形式で記述）</summary>
        public const string WorkBreakCloseTime = "WorkBreakCloseTime";
        /// <summary>就業終了時刻（HH:mm形式で記述）</summary>
        public const string WorkCloseTime = "WorkCloseTime";
        /// <summary>画像品質値(最低0-最高100)、-1は変換なし固定</summary>
        public const string ImageQualityNum = "ImageQualityNum";
        /// <summary>スケジューリング配信時間確認間隔（ミリ秒）</summary>
        public const string ConfirmLatestSchedulingPolingTime = "ConfirmLatestSchedulingPolingTime";
        /// <summary>直近スケジューリング配信日時</summary>
        public const string LatestSchedulingTime = "LatestSchedulingTime";
        /// <summary>MBOMインポートの初期パス</summary>
        public const string MBOMImportDefaultPath = "MBOMImportDefaultPath";
        /// <summary>MBOMインポートファイル名</summary>
        public const string MBOMImportFileName = "MBOMImportFileName";
        /// <summary>ナレッジ管理の図面フォルダパス</summary>
        public const string DiagramCategoryPath = "DiagramCategoryPath";
        /// <summary>MBOM工具データインポートの初期パス</summary>
        public const string NCToolImportDefaultPath = "NCToolImportDefaultPath";
        /// <summary>作業実績登録、グループ別作業実績登録、計画外作業登録で適用ボタン押下時にDB登録するか</summary>
        public const string EnableApplyDBUpdate = "EnableApplyDBUpdate";
        /// <summary>カメラでQRコード読取を可能とするかどうか</summary>
        public const string EnableQRRead = "EnableQRRead";
        /// <summary>実績情報の連携をスキップするかどうか。</summary>
        public const string SkipResultConnect = "SkipResultConnect";
        /// <summary>帳票出力先フォルダ</summary>
        public const string ReportSavePath = "ReportSavePath";
        /// <summary>現品票のテンプレート</summary>
        public const string TemplatePath = "TemplatePath";
        /// <summary>印刷済み配置フォルダ</summary>
        public const string PrintedReportPath = "PrintedReportPath";
        /// <summary>オーダー一覧の検索時の最大取得行数</summary>
        public const string OrderListTopRowLimit = "OrderListTopRowLimit";
        /// <summary>ナレッジコンテンツ一覧の最大取得行数</summary>
        public const string ContentsListTopRowLimit = "ContentsListTopRowLimit";
        /// <summary>NKT'：中田製作所用Proxyを利用, 'Default'：DefaultProxyを利用</summary>
        public const string DIProxyType = "DIProxyType";
        /// <summary>受注明細の最大（有効）行数</summary>
        public const string SalesOrderLineRowLimit = "SalesOrderLineRowLimit";
        /// <summary>販売管理機能を有効とするか</summary>
        public const string EnableSalesControl = "EnableSalesControl";
        /// <summary>会計年度の開始月</summary>
        public const string FiscalYearStartMonth = "FiscalYearStartMonth";
        /// <summary>受注の表示名（default:受注)</summary>
        public const string SalesOrderName = "SalesOrderName";
        /// <summary>受注TBL社内注文Idの最大値</summary>
        public const string CompanyOrderIdLimit = "CompanyOrderIdLimit";
        /// <summary>社内注文書系帳票出力先フォルダ</summary>
        public const string SalesOrderReportSavePath = "SalesOrderReportSavePath";
        /// <summary>社内注文書系印刷済み配置フォルダ</summary>
        public const string SalesOrderPrintedReportPath = "SalesOrderPrintedReportPath";
        /// <summary>表面処理注文書のテンプレートパス</summary>
        public const string SurfaceOrderReportTemplatePath = "SurfaceOrderReportTemplatePath";
        /// <summary>ヘリサート必要予定表のテンプレートパス</summary>
        public const string HelicoidScheduleReportTemplatePath = "HelicoidScheduleReportTemplatePath";
        /// <summary>材料注文書のテンプレートパス</summary>
        public const string MaterialOrderReportTemplatePath = "MaterialOrderReportTemplatePath";
        /// <summary>社内注文書のテンプレートパス</summary>
        public const string SalesOrderReportTemplatePath = "SalesOrderReportTemplatePath";
        /// <summary>注文金額照会画面検索時の最大取得行数</summary>
        public const string SalesOrderAmountListTopRowLimit = "SalesOrderAmountListTopRowLimit";
        /// <summary>MES専用プリンタの名称。存在しない場合はデフォルトプリンタが使われる。</summary>
        public const string MESPrinter = "MESPrinter";
        /// <summary>特定作業一括強制完了の対象となる作業コードの部分文字列（'#Disabled#'の場合は無効）</summary>
        public const string SpecificOperationStr = "SpecificOperationStr";
        /// <summary>オーダー引当て作業を表示するか</summary>
        public const string EnableOrderPeggingOperation = "EnableOrderPeggingOperation";
        /// <summary>オーダ一覧で工程をスライド表示するか</summary>
        public const string EnableProcSlideOrderForm = "EnableProcSlideOrderForm";
        /// <summary>受注明細のエクスポートパス</summary>
        public const string SalesOrderExportPath = "SalesOrderExportPath";
        /// <summary>受注明細のエクスポートファイル名</summary>
        public const string SalesOrderExportFileName = "SalesOrderExportFileName";
        /// <summary>在庫管理機能を有効とするか？</summary>
        public const string EnableInventoryControl = "EnableInventoryControl";
        /// <summary>モバイル版実績メモ一覧パネルの1ページ当たりの最大パネル数</summary>
        public const string MaxResultmMemoPanelCount = "MaxResultmMemoPanelCount";
        /// <summary>棚卸表出力先フォルダ</summary>	
        public const string InventoryReportSavePath = "InventoryReportSavePath";
        /// <summary>棚卸表のテンプレート</summary>	
        public const string InventoryTemplatePath = "InventoryTemplatePath";
        /// <summary>棚卸表印刷済み配置フォルダ</summary>	
        public const string InventoryPrintedReportPath = "InventoryPrintedReportPath";
        /// <summary>受注明細未作成(SkyBlue)</summary>
        public const string ColorSlsOdrNCRT = "ColorSlsOdrNCRT";
        /// <summary>受注明細作成済(White)</summary>
        public const string ColorSlsOdrFCRT = "ColorSlsOdrFCRT";
        /// <summary>受注明細社内注文書発行済(Aquamarine)</summary>
        public const string ColorSlsOdrFSOP = "ColorSlsOdrFSOP";
        /// <summary>受注明細社内事務コンより取込み(LightGray)</summary>
        public const string ColorSlsOdrFSOE = "ColorSlsOdrFSOE";
        /// <summary>作業コードの末尾が"_入庫" or "_出庫"の時に、入出庫入力を促すかどうか？</summary>
        public const string EnableInventoryPrompt = "EnableInventoryPrompt";
        /// <summary>入庫作業PostFix</summary>
        public const string InPostFix = "InPostFix";
        /// <summary>出庫作業PostFix</summary>
        public const string OutPostFix = "OutPostFix";
        /// <summary>NTN工程管理票出力先フォルダ</summary>
        public const string NTNProcReportSavePath = "NTNProcReportSavePath";
        /// <summary>NTN工程管理票印刷済み配置フォルダ</summary>
        public const string NTNProcPrintedReportPath = "NTNProcPrintedReportPath";
        /// <summary>NTN場外工程管理票のテンプレートパス</summary>	
        public const string NTNJyogaiProcReportTemplatePath = "NTNJyogaiProcReportTemplatePath";
        /// <summary>NTN場内工程管理票のテンプレートパス</summary>
        public const string NTNJyonaiProcReportTemplatePath = "NTNJyonaiProcReportTemplatePath";
        /// <summary>NTN組立工程管理票のテンプレートパス</summary>
        public const string NTNKumitateProcReportTemplatePath = "NTNKumitateProcReportTemplatePath";
        /// <summary>NTN中期計画表出力先フォルダ</summary>
        public const string NTNMidReportSavePath = "NTNMidReportSavePath";
        /// <summary>NTN中期計画表印刷済み配置フォルダ</summary>
        public const string NTNMidPrintedReportPath = "NTNMidPrintedReportPath";
        /// <summary>NTN中期計画表のテンプレートパス</summary>
        public const string NTNMidReportTemplatePath = "NTNMidReportTemplatePath";
        /// <summary>NTN現品票材入票出力先フォルダ</summary>
        public const string NTNTagReportSavePath = "NTNTagReportSavePath";
        /// <summary>NTN工程管理票印刷済み配置フォルダ</summary>
        public const string NTNTagPrintedReportPath = "NTNTagPrintedReportPath";
        /// <summary>NTN現品票のテンプレートパス</summary>
        public const string NTNTagIdentificationReportTemplatePath = "NTNTagIdentificationReportTemplatePath";
        /// <summary>NTN材入票のテンプレートパス</summary>
        public const string NTNTagMaterialReportTemplatePath = "NTNTagMaterialReportTemplatePath";
        /// <summary>未割付セルの色(各担当パネル)</summary>
        public const string ColorNonePlanned = "ColorNonePlanned";
        /// <summary>予定セルの色(各担当パネル)</summary>
        public const string ColorPlanned = "ColorPlanned";
        /// <summary>実績セル以外の色(各担当パネル)</summary>
        public const string ColorNoneResult = "ColorNoneResult";
        /// <summary>予定内に完了セルの色(見える化パネル)</summary>
        public const string ColorResultInPlanned = "ColorResultInPlanned";
        /// <summary>遅れて完了セルの色(見える化パネル)</summary>
        public const string ColorResultDelayed = "ColorResultDelayed";
        /// <summary>実施中セルの色(見える化パネル)</summary>
        public const string ColorWorking = "ColorWorking";
        /// <summary>遅れ中セルの色(見える化パネル)</summary>
        public const string ColorDelaying = "ColorDelaying";
        /// <summary>工程が存在しないセルの色(加工前準備計画パネル、場内計画パネル)</summary>
        public const string ColorNotExistsProc = "ColorNotExistsProc";
        /// <summary>工程名が設定値（コロンで複数設定可）を含む場合、場外工程管理票の表示対象外とし、場内工程管理票を追加発行する</summary>
        public const string NTNJyonaiAddingOutputProcs = "NTNJyonaiAddingOutputProcs";
        /// <summary>表示対象の出荷工程名(加工前準備計画パネル)</summary>
        public const string ShukkaProcCode = "ShukkaProcCode";
        /// <summary>材料計算式および算式のExcelパス</summary>
        public const string NKTMaterialCalcExcelPath = "NKTMaterialCalcExcelPath";
        /// <summary>直接リンクファイルのBaseURL</summary>
        public const string DirectLinkBaseURL = "DirectLinkBaseURL";
        /// <summary>直接リンクファイルのBaseUNCPath</summary>
        public const string DirectLinkBaseUNCPath = "DirectLinkBaseUNCPath";
        /// <summary>コードマスタのDirectLinkRefPath設定なしの場合のユーザー参照初期ディレクトリ</summary>
        public const string DefaultDirectLinkRefPath = "DefaultDirectLinkRefPath";
        /// <summary>直接リンクファイル選択後、選択されたファイルを削除するか</summary>
        public const string EnableDeteleSelectedRefDirectLinkFile = "EnableDeteleSelectedRefDirectLinkFile";
        /// <summary>原価管理機能を有効とするか</summary>
        public const string EnableCostControl = "EnableCostControl";
        /// <summary>メインメニューのFLEXSCHEアイコンを表示するか(メインメニューでの表示非表示のみ）</summary>
        public const string VisibleFLexscheMainFrame = "VisibleFLexscheMainFrame";

        #endregion CommonConf

        /// <summary>作業実績ステータス</summary>
        #region ResultStatus
        /// <summary>実績なし</summary>
        public const string ResultStatusN = "N";
        /// <summary>前段取り中</summary>
        public const string ResultStatusSS = "SS";
        /// <summary>前段取り完了</summary>
        public const string ResultStatusSF = "SF";
        /// <summary>製造中</summary>
        public const string ResultStatusMS = "MS";
        /// <summary>製造完了</summary>
        public const string ResultStatusMF = "MF";
        /// <summary>後段取り中</summary>
        public const string ResultStatusTS = "TS";
        /// <summary>全完了</summary>
        public const string ResultStatusTF = "TF";
        #endregion ResultStatus

        /// <summary>対象パート</summary>
        #region TargetPart
        /// <summary>作業全体</summary>
        public const string TargetPartOpe = "Ope";
        /// <summary>前段取り</summary>
        public const string TargetPartSetup = "Setup";
        /// <summary>製造</summary>
        public const string TargetPartManu = "Manu";
        /// <summary>後段取り</summary>
        public const string TargetPartTeardown = "Teardown";
        #endregion TargetPart

        /// <summary>作業パート</summary>
        #region WorkPart
        /// <summary>間接作業</summary>
        public const string WorkPartIndirect = "I";
        /// <summary>前段取り</summary>
        public const string WorkPartSetup = "S";
        /// <summary>製造</summary>
        public const string WorkPartManu = "M";
        /// <summary>後段取り</summary>
        public const string WorkPartTeardown = "T";
        #endregion WorkPart

        /// <summary>作業実績明細区分</summary>
        #region ResultDetailDiv
        /// <summary>開始</summary>
        public const string DetailDivStart = "S";
        /// <summary>中断</summary>
        public const string DetailDivPause = "P";
        /// <summary>再開</summary>
        public const string DetailDivReStart = "RS";
        /// <summary>パート完了</summary>
        public const string DetailDivPartFinish = "PF";
        /// <summary>全完了</summary>
        public const string DetailDivFinish = "F";
        /// <summary>仕掛中</summary>
        public const string DetailDivInProc = "INPRC";
        #endregion ResultDetailDiv

        /// <summary>抽出範囲</summary>
        #region ExtractBound
        /// <summary>範囲内</summary>
        public const string ExtractBoundIn = "In";
        /// <summary>範囲外</summary>
        public const string ExtractBoundOut = "Out";
        #endregion ExtractBound

        /// <summary>作業着手状況</summary>
        #region ProgressStatus
        /// <summary>開始前</summary>
        public const string ProgressStatusPRE = "PRE";
        /// <summary>開始</summary>
        public const string ProgressStatusS = "S";
        /// <summary>着手遅れ</summary>
        public const string ProgressStatusSDLY = "SDLY";
        /// <summary>完了遅れ</summary>
        public const string ProgressStatusFDLY = "FDLY";
        /// <summary>完了済</summary>
        public const string ProgressStatusFIN = "FIN";
        #endregion ProgressStatus

        /// <summary>見込みレベル</summary>
        #region ProspectLevel
        /// <summary>受注済</summary>
        public const int ProspectLevelOrdered = 0;
        /// <summary>先行着手</summary>
        public const int ProspectLevelPrecedingStart = 10;
        /// <summary>引合い中</summary>
        public const int ProspectLevelInquiry = 20;
        #endregion ProspectLevel

        /// <summary>拡張列タイプ</summary>
        #region ExtcolType
        /// <summary>仕様制約</summary>
        public const string ExtcolTypeSpecs = "specs";
        /// <summary>数値仕様制約</summary>
        public const string ExtcolTypeNumSpecs = "numspecs";
        /// <summary>仕様</summary>
        public const string ExtcolTypeGSpecs = "g-specs";
        /// <summary>数値仕様</summary>
        public const string ExtcolTypeGNumSpecs = "g-numspecs";
        /// <summary>フラグ</summary>
        public const string ExtcolTypeFlags = "flags";
        /// <summary>コメント（日付）</summary>
        public const string ExtcolTypeCommentsDate = "comments-date";
        /// <summary>コメント（文字列）</summary>
        public const string ExtcolTypeCommentsString = "comments-string";
        #endregion ExtcolType


        /// <summary>パラメータDataTable名</summary>
        #region ParameterTableName
        public const string RateTbl = "RateTable";
        public const string OrderWorkStatusTbl = "OrderWorkStatus";
        public const string PlanMemoStatusTbl = "PlanMemoStatus";
        public const string WorkStatusTbl = "WorkStatus";
        public const string OrgContentsTbl = "OrgContentsTbl";
        public const string ExtraParamTbl = "ExtraParamTbl";
        public const string ServerTimeTbl = "ServerTimeTbl";
        public const string SupplierRelationTbl = "SupplierRelationTbl";
        public const string SalesOrderLineStatusTbl = "SalesOrderLineStatusTbl";
        public const string MaterialCostTbl = "MaterialCostTbl";
        #endregion ParameterTableName

        /// <summary>添付ファイルタイプ</summary>
        #region AppendType
        public const string AppendTypePlanMemo = "PlanMemo";
        public const string AppendTypeResultMemo = "ResultMemo";
        public const string AppendTypeFailureInfo = "FailureInfo";
        public const string AppendTypeKnowledge = "Knowledge";
        #endregion AppendType

        /// <summary>全文検索対象ディレクトリ</summary>
        #region TargetDir
        public const string TargetMemoDir = @"\Repo\Memo";
        public const string TargetFailureInfoDir = @"\Repo\FailureInfo";
        public const string TargetKnowledgeDir = @"\Repo\Knowledge";
        #endregion TargetDir

        /// <summary>メモ添付のLuceneIndex</summary>
        #region MemoIndex
        public const string MemoIndexMain = @"\LuceneIndex\Memo\Main";
        public const string MemoIndexSub = @"\LuceneIndex\Memo\Sub";
        public const string MemoIndexBuild = @"\LuceneIndex\Memo\Build";
        #endregion MemoIndex

        /// <summary>不良情報添付のLuceneIndex</summary>
        #region FailureInfoIndex
        /// <summary>インデックス配置場所</summary>
        public const string FailureInfoIndexMain = @"\LuceneIndex\FailureInfo\Main";
        /// <summary>置換対象インデックス配置場所</summary>
        public const string FailureInfoIndexSub = @"\LuceneIndex\FailureInfo\Sub";
        /// <summary>構築中インデックス配置場所</summary>
        public const string FailureInfoIndexBuild = @"\LuceneIndex\FailureInfo\Build";
        #endregion FailureInfoIndex

        /// <summary>ナレッジ添付のLuceneIndex</summary>
        #region KnowledgeIndex
        /// <summary>インデックス配置場所</summary>
        public const string KnowledgeIndexMain = @"\LuceneIndex\Knowledge\Main";
        /// <summary>置換対象インデックス配置場所</summary>
        public const string KnowledgeIndexSub = @"\LuceneIndex\Knowledge\Sub";
        /// <summary>構築中インデックス配置場所</summary>
        public const string KnowledgeIndexBuild = @"\LuceneIndex\Knowledge\Build";
        #endregion KnowledgeIndex

        /// <summary>アラート種別</summary>
        #region AlertType
        /// <summary>納期逼迫</summary>
        public const string AlertTypeDeadlineImpending = "DeadlineImpending";
        /// <summary>品目別不良率</summary>
        public const string AlertTypeFailureRate = "FailureRate";
        /// <summary>品目別リードタイム</summary>
        public const string AlertTypeLeadTime = "LeadTime";
        #endregion AlertType

        /// <summary>コントロールタイプ</summary>
        #region ControlType
        /// <summary>所属グループ</summary>
        public const string CtlTypeGroupList = "GL";
        /// <summary>品目所属グループ</summary>
        public const string CtlTypeItemGroupList = "IG";
        #endregion ControlType

        /// <summary>実績入力モード</summary>
        /// HACK 命名規約違反を修正
        #region ResultInputMode
        /// <summary>通常</summary>
        public const string ResultInputMode_NORMAL = "NORMAL";
        /// <summary>通常（製造のみ）</summary>
        public const string ResultInputMode_NORMAL_MANU = "NORMAL_MANU";
        /// <summary>簡易（着完）</summary>
        public const string ResultInputMode_SIMPLE_STARTEND = "SIMPLE_STARTEND";
        /// <summary>簡易（完了のみ）</summary>
        public const string ResultInputMode_SIMPLE_END = "SIMPLE_END";
        #endregion ResultInputMode

        /// <summary>作業実績種別</summary>
        #region ResultType
        /// <summary>実績作業として指定</summary>
        public const string ResultTypeR = "R";
        #endregion ResultType

        /// <summary>全文検索結果ハイライト用</summary>
        #region FTSHighlight
        /// <summary>全文検索結果ハイライト用データテーブル列付加文字列</summary>
        public const string KeywordMatched = "_keyword_matched";
        #endregion FTSHighlight

        /// <summary>行ハイライト用</summary>
        #region RowHighlight
        public const string RowHighLight = "row_highlight"; //行ハイライト用列名
        #endregion RowHighlight

        /// <summary>工程時間単位</summary>
        #region ProcTimeUnit
        /// <summary>日付単位工程</summary>
        public const string ProcTimeUnitDAY = "DAYUNIT";
        /// <summary>分単位工程</summary>
        public const string ProcTimeUnitMIN = "MINUNIT";
        #endregion ProcTimeUnit

        /// <summary>仕入先連関</summary>
        #region SupplierRelation

        /// <summary>材料仕入先</summary>
        public const string SupplierRelationMATERIAL = "MATERIAL";
        /// <summary>表面処理依頼先</summary>
        public const string SupplierRelationSURFACE = "SURFACE";

        #endregion SupplierRelation

        /// <summary>工程属性</summary>
        #region ProcAttribute
        /// <summary>NC機工程</summary>
        public const string ProcAttributeNCProc = "NC_PROC";
        /// <summary>選択不可資源</summary>
        public const string ProcAttributeNotSelect = "NOT_SELECT";
        #endregion ProcAttribute

        /// <summary>入出庫区分</summary>
        #region InOutDiv
        /// <summary>入庫</summary>
        public const string InOutDivPlannedIn = "PlannedIn";
        /// <summary>出庫</summary>
        public const string InOutDivPlannedOut = "PlannedOut";
        /// <summary>入庫(計画外)</summary>
        public const string InOutDivUnplannedIn = "UnplannedIn";
        /// <summary>出庫(計画外)</summary>
        public const string InOutDivUnplannedOut = "UnplannedOut";
        /// <summary>廃棄</summary>
        public const string InOutDivDiscard = "Discard";
        /// <summary>移動出庫</summary>
        public const string InOutDivMoveOut = "MoveOut";
        /// <summary>移動入庫</summary>
        public const string InOutDivMoveIn = "MoveIn";
        /// <summary>棚卸</summary>
        public const string InOutDivPhisicalInventory = "PhisicalInventory";
        #endregion InOutDiv

        /// <summary>ロットステータス</summary>
        #region LotStatus
        /// <summary>未判定</summary>
        public const string LotStatusUndecided = "Undecided";
        /// <summary>合格</summary>
        public const string LotStatusSuccess = "Success";
        /// <summary>不合格</summary>
        public const string LotStatusFailure = "Failure";
        #endregion LotStatus

        /// <summary>受注IDの仮初期値(新規値)</summary>
        public const int DefaultSalesOrderId = int.MinValue;

        /// <summary>受注明細ステータス</summary>
        #region SalesOrderLineStatus
        /// <summary>未作成</summary>
        public const string SalesOrderLineStatusUnCreated = "NCRT";
        /// <summary>作成済</summary>
        public const string SalesOrderLineStatusCreated = "FCRT";
        /// <summary>帳票出力済</summary>
        public const string SalesOrderLineStatusReportPrinted = "FSOP";
        /// <summary>ERP連携済</summary>
        public const string SalesOrderLineStatusERPCoordinated = "FSOE";
        #endregion SalesOrderLineStatus

        /// <summary>ロック実施するか</summary>
        public const string DoLock = "DO_LOCK"; //ロックする


        /// <summary>Zipファイル名</summary>
        public const string FlexscheZipName = "FLXPJ.zip";

        /// <summary>入力チェック用最大桁</summary>
        #region ColumnLengthForInputCheck

        /// <summary>工程コードの最大桁(10)</summary>
        public const int ProcCdMaxLength = 10;
        /// <summary>NCPgNoの最大桁(30)</summary>
        public const int NCPgNoMaxLength = 30;
        /// <summary>TNoの最大桁(30)</summary>
        public const int TNoMaxLength = 30;
        /// <summary>ToolNameの最大桁(30)</summary>
        public const int ToolNameMaxLength = 30;
        /// <summary>HNoの最大桁(30)</summary>
        public const int HNoMaxLength = 30;
        /// <summary>DNoの最大桁(30)</summary>
        public const int DNoMaxLength = 30;
        /// <summary>ToolNoteの最大桁(2000)</summary>
        public const int ToolNoteMaxLength = 2000;

        #endregion

        /// <summary>MES用拡張列タイプ</summary>
        #region FxExtcolDataType
        /// <summary>文字列型</summary>
        public const string FxExtcolDataTypeString = "S";
        /// <summary>日付型</summary>
        public const string FxExtcolDataTypeDate = "D";
        /// <summary>浮動小数型</summary>
        public const string FxExtcolDataTypeFloat = "F";
        /// <summary>整数型</summary>
        public const string FxExtcolDataTypeInteger = "I";
        /// <summary>論理型</summary>
        public const string FxExtcolDataTypeBool = "B";
        #endregion FxExtcolDataType

        #region DIProxyType
        /// <summary>デフォルトDIプロキシ</summary>
        public const string DIProxyTypeDefault = "Default";
        /// <summary>中田製作所用DIプロキシ</summary>
        public const string DIProxyTypeNKT = "NKT";
        /// <summary>NTN用DIプロキシ</summary>
        public const string DIProxyTypeNTN = "NTN";
        #endregion DIProxyType

        /// <summary>環境変数</summary>
        #region EnvironmentVariable
        /// <summary>PDF編集アプリケーションパス環境変数</summary>
        public const string MES_PDF_EDITOR = "MES_PDF_EDITOR";
        #endregion EnvironmentVariable

        /// <summary>外部プログラム連携用のSessionId</summary>
        public const string OuterProcessSessionId = "14AA38BABA544E799";

        /// <summary>禁止文字を全角文字に置換</summary>
        public static string ReplaceNGWord2Wide(string value) {
            //全体禁止文字
            value = value.Replace("/", "／");
            value = value.Replace("&", "＆");
            value = value.Replace("\\", "￥");
            value = value.Replace("<", "＜");
            value = value.Replace(">", "＞");
            value = value.Replace(",", "，");
            value = value.Replace(";", "；");
            value = value.Replace("\"", "”");
            value = value.Replace("'", "’");
            //HACK NKTのEDIF変換を外したタイミングで外す
            value = value.Replace("#", "＃");

            //先頭禁止文字
            value = Regex.Replace(value, "^#.*", "＃" + value.Substring(1));
            value = Regex.Replace(value, "^@.*", "＠" + value.Substring(1));
            value = Regex.Replace(value, "^%.*", "％" + value.Substring(1));
            value = Regex.Replace(value, "^-.*", "－" + value.Substring(1));
            //value = Regex.Replace(value, "^\\.*", "￥" + value.Substring(1)); //前段で置換されるはず
            value = Regex.Replace(value, "^!.*", "！" + value.Substring(1));
            value = Regex.Replace(value, "^=.*", "＝" + value.Substring(1));

            return value;
        }

        /// <summary>禁止文字を含んでいるか</summary>
        public static bool ContainsNGWord(string value) {
            if (value.IndexOf("/") >= 0 ||
                value.IndexOf("&") >= 0 ||
                value.IndexOf("\\") >= 0 ||
                value.IndexOf("<") >= 0 ||
                value.IndexOf(">") >= 0 ||
                value.IndexOf(",") >= 0 ||
                value.IndexOf(";") >= 0 ||
                //HACK NKTのEDIF変換を外したタイミングで外す
                value.IndexOf("#") >= 0 ||
                value.IndexOf("\"") >= 0 ||
                value.IndexOf("'") >= 0) {
                return true;
            }
            return false;
        }

        /// <summary>禁止文字置換</summary>
        public static string RemoveNGWord(string value) {
            value = value.Replace("/", "");
            value = value.Replace("&", "");
            value = value.Replace("\\", "");
            value = value.Replace("<", "");
            value = value.Replace(">", "");
            value = value.Replace(",", "");
            value = value.Replace(";", "");
            //HACK NKTのEDIF変換を外したタイミングで外す
            value = value.Replace("#", "");
            value = value.Replace("\"", "");
            value = value.Replace("'", "");

            return value;
        }

        /// <summary>先頭禁止文字を含んでいるか</summary>
        public static bool ContainsTopNGWord(string value) {
            if (Regex.IsMatch(value, "^#.*") ||
                Regex.IsMatch(value, "^@.*") ||
                Regex.IsMatch(value, "^%.*") ||
                Regex.IsMatch(value, "^-.*") ||
                Regex.IsMatch(value, @"^\\.*") ||
                Regex.IsMatch(value, "^!.*") ||
                Regex.IsMatch(value, "^=.*")) {
                return true;
            }

            return false;
        }

        /// <summary>特定作業一括強制完了の無効化文字列</summary>
        public static string DisabledForceCompleteSpecificOperationStr = "#Disabled#";
    }
}
