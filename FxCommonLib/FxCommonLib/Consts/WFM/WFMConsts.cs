using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Consts.WFM {
    public static class WFMConsts {

        /// <summary>データディクショナリ</summary>
        #region DataDictionary
        public const string employee_id = "employee_id"; //従業員ID
        public const string employee_name = "employee_name"; //従業員名
        public const string employee_kana = "employee_kana"; //カナ
        public const string authority_code = "authority_code"; //権限コード
        public const string dep_code = "dep_code"; //部署コード
        public const string main_group_id = "main_group_id"; //メイングループID
        public const string employment_type = "employment_type"; //雇用形態
        public const string supported = "supported"; //扶養希望
        public const string com_code = "com_code"; //会社コード
        public const string hourly_wage = "hourly_wage"; //時給
        public const string transportation_cost = "transportation_cost"; //交通費
        public const string commuting_minutes = "commuting_minutes"; //通勤時間（分）
        public const string note = "note"; //備考
        public const string password = "password"; //パスワード
        public const string pwd_up_id = "pwd_up_id"; //パスワード変更従業員ID
        public const string pwd_up_name = "pwd_up_name"; //パスワード変更従業員名
        public const string pwd_up_date = "pwd_up_date"; //パスワード変更日時
        public const string last_login_date = "last_login_date"; //最終ログイン日時
        public const string message_id = "message_id"; //メッセージID
        public const string message = "message"; //メッセージ
        public const string col_name = "col_name"; //列名
        public const string db_name = "db_name"; //物理名
        public const string insert_id = "insert_id"; //登録者ID
        public const string insert_date = "insert_date"; //登録日時
        public const string update_id = "update_id"; //更新者ID
        public const string update_date = "update_date"; //更新日時
        public const string delete_id = "delete_id"; //削除者ID
        public const string delete_date = "delete_date"; //削除日時
        public const string delete_flg = "delete_flg"; //削除フラグ
        public const string session_id = "session_id"; //セッションID
        public const string admin_menu_lock = "admin_menu_lock"; //管理者メニューロック
        public const string window_name = "window_name"; //Window名
        public const string control_name = "control_name"; //コントロール名
        public const string conf_value = "conf_value"; //設定値
        public const string visible = "visible"; //表示
        public const string ctl_type = "ctl_type"; //コントロールタイプ
        public const string value1 = "value1"; //値1
        public const string value2 = "value2"; //値2
        public const string disp_order = "disp_order"; //表示順
        public const string code = "code"; //コード
        public const string name = "name"; //名称
        public const string grid_name = "grid_name"; //グリッド名
        public const string snapshot_id = "snapshot_id"; //スナップショットID
        public const string snapshot_name = "snapshot_name"; //スナップショット名
        public const string start_date = "start_date"; //開始日
        public const string end_date = "end_date"; //終了日
        public const string days = "days"; //日数
        public const string calc_sec = "calc_sec"; //計算時間
        public const string insert_name = "insert_name"; //登録者名
        public const string update_name = "update_name"; //更新者名
        public const string value = "value"; //値
        public const string width = "width"; //列幅
        public const string height = "height"; //高さ
        public const string conf_editable = "conf_editable"; //設定編集
        public const string editable = "editable"; //編集可能フラグ
        public const string col_fixed = "col_fixed"; //列固定
        public const string data_type = "data_type"; //データタイプ
        public const string max_length = "max_length"; //最大桁数
        public const string required = "required"; //必須
        public const string primary_key = "primary_key"; //プライマリキー
        public const string password_char = "password_char"; //パスワード文字
        public const string content = "content"; //内容
        public const string delete_name = "delete_name"; //削除者名
        public const string enabled = "enabled"; //有効
        public const string day_of_week = "day_of_week"; //対象曜日
        public const string update_key = "update_key"; //update_key
        public const string rule_code = "rule_code"; //ルールコード
        public const string employee1 = "employee1"; //従業員1
        public const string employee2 = "employee2"; //従業員2
        public const string shift_code = "shift_code"; //シフトコード
        public const string shift_pattern_code = "shift_pattern_code"; //シフトパターンコード
        public const string category = "category"; //カテゴリ
        public const string constraint_name = "constraint_name"; //制約名
        public const string impact = "impact"; //影響度
        public const string item0_name = "item0_name"; //i日目項目名
        public const string item1_name = "item1_name"; //i+1日目項目名
        public const string item2_name = "item2_name"; //i+2日目項目名
        public const string item3_name = "item3_name"; //i+3日目項目名
        public const string item4_name = "item4_name"; //i+4日目項目名
        public const string item5_name = "item5_name"; //i+5日目項目名
        public const string item6_name = "item6_name"; //i+6日目項目名
        public const string item7_name = "item7_name"; //i+7日目項目名
        public const string item8_name = "item8_name"; //i+8日目項目名
        public const string item9_name = "item9_name"; //i+9日目項目名
        public const string category_name = "category_name"; //カテゴリ名
        public const string abbreviation = "abbreviation"; //略号
        public const string color = "color"; //色
        public const string expansion = "expansion"; //拡張列
        public const string shift_type_flag = "shift_type_flag"; //シフト種別フラグ
        public const string serial_number = "serial_number"; //連番
        public const string group = "group"; //グループ
        public const string role = "role"; //役割
        public const string period = "period"; //コマ
        public const string period1 = "period1"; //0:00
        public const string period2 = "period2"; //0:15
        public const string period3 = "period3"; //0:30
        public const string period4 = "period4"; //0:45
        public const string period5 = "period5"; //1:00
        public const string period6 = "period6"; //1:15
        public const string period7 = "period7"; //1:30
        public const string period8 = "period8"; //1:45
        public const string period9 = "period9"; //2:00
        public const string period10 = "period10"; //2:15
        public const string period11 = "period11"; //2:30
        public const string period12 = "period12"; //2:45
        public const string period13 = "period13"; //3:00
        public const string period14 = "period14"; //3:15
        public const string period15 = "period15"; //3:30
        public const string period16 = "period16"; //3:45
        public const string period17 = "period17"; //4:00
        public const string period18 = "period18"; //4:15
        public const string period19 = "period19"; //4:30
        public const string period20 = "period20"; //4:45
        public const string period21 = "period21"; //5:00
        public const string period22 = "period22"; //5:15
        public const string period23 = "period23"; //5:30
        public const string period24 = "period24"; //5:45
        public const string period25 = "period25"; //6:00
        public const string period26 = "period26"; //6:15
        public const string period27 = "period27"; //6:30
        public const string period28 = "period28"; //6:45
        public const string period29 = "period29"; //7:00
        public const string period30 = "period30"; //7:15
        public const string period31 = "period31"; //7:30
        public const string period32 = "period32"; //7:45
        public const string period33 = "period33"; //8:00
        public const string period34 = "period34"; //8:15
        public const string period35 = "period35"; //8:30
        public const string period36 = "period36"; //8:45
        public const string period37 = "period37"; //9:00
        public const string period38 = "period38"; //9:15
        public const string period39 = "period39"; //9:30
        public const string period40 = "period40"; //9:45
        public const string period41 = "period41"; //10:00
        public const string period42 = "period42"; //10:15
        public const string period43 = "period43"; //10:30
        public const string period44 = "period44"; //10:45
        public const string period45 = "period45"; //11:00
        public const string period46 = "period46"; //11:15
        public const string period47 = "period47"; //11:30
        public const string period48 = "period48"; //11:45
        public const string period49 = "period49"; //12:00
        public const string period50 = "period50"; //12:15
        public const string period51 = "period51"; //12:30
        public const string period52 = "period52"; //12:45
        public const string period53 = "period53"; //13:00
        public const string period54 = "period54"; //13:15
        public const string period55 = "period55"; //13:30
        public const string period56 = "period56"; //13:45
        public const string period57 = "period57"; //14:00
        public const string period58 = "period58"; //14:15
        public const string period59 = "period59"; //14:30
        public const string period60 = "period60"; //14:45
        public const string period61 = "period61"; //15:00
        public const string period62 = "period62"; //15:15
        public const string period63 = "period63"; //15:30
        public const string period64 = "period64"; //15:45
        public const string period65 = "period65"; //16:00
        public const string period66 = "period66"; //16:15
        public const string period67 = "period67"; //16:30
        public const string period68 = "period68"; //16:45
        public const string period69 = "period69"; //17:00
        public const string period70 = "period70"; //17:15
        public const string period71 = "period71"; //17:30
        public const string period72 = "period72"; //17:45
        public const string period73 = "period73"; //18:00
        public const string period74 = "period74"; //18:15
        public const string period75 = "period75"; //18:30
        public const string period76 = "period76"; //18:45
        public const string period77 = "period77"; //19:00
        public const string period78 = "period78"; //19:15
        public const string period79 = "period79"; //19:30
        public const string period80 = "period80"; //19:45
        public const string period81 = "period81"; //20:00
        public const string period82 = "period82"; //20:15
        public const string period83 = "period83"; //20:30
        public const string period84 = "period84"; //20:45
        public const string period85 = "period85"; //21:00
        public const string period86 = "period86"; //21:15
        public const string period87 = "period87"; //21:30
        public const string period88 = "period88"; //21:45
        public const string period89 = "period89"; //22:00
        public const string period90 = "period90"; //22:15
        public const string period91 = "period91"; //22:30
        public const string period92 = "period92"; //22:45
        public const string period93 = "period93"; //23:00
        public const string period94 = "period94"; //23:15
        public const string period95 = "period95"; //23:30
        public const string period96 = "period96"; //23:45
        public const string period97 = "period97"; //24:00
        public const string period98 = "period98"; //24:15
        public const string period99 = "period99"; //24:30
        public const string period100 = "period100"; //24:45
        public const string period101 = "period101"; //25:00
        public const string period102 = "period102"; //25:15
        public const string period103 = "period103"; //25:30
        public const string period104 = "period104"; //25:45
        public const string period105 = "period105"; //26:00
        public const string period106 = "period106"; //26:15
        public const string period107 = "period107"; //26:30
        public const string period108 = "period108"; //26:45
        public const string period109 = "period109"; //27:00
        public const string period110 = "period110"; //27:15
        public const string period111 = "period111"; //27:30
        public const string period112 = "period112"; //27:45
        public const string period113 = "period113"; //28:00
        public const string period114 = "period114"; //28:15
        public const string period115 = "period115"; //28:30
        public const string period116 = "period116"; //28:45
        public const string period117 = "period117"; //29:00
        public const string period118 = "period118"; //29:15
        public const string period119 = "period119"; //29:30
        public const string period120 = "period120"; //29:45
        public const string period121 = "period121"; //30:00
        public const string period122 = "period122"; //30:15
        public const string period123 = "period123"; //30:30
        public const string period124 = "period124"; //30:45
        public const string period125 = "period125"; //31:00
        public const string period126 = "period126"; //31:15
        public const string period127 = "period127"; //31:30
        public const string period128 = "period128"; //31:45
        public const string period129 = "period129"; //32:00
        public const string period130 = "period130"; //32:15
        public const string period131 = "period131"; //32:30
        public const string period132 = "period132"; //32:45
        public const string period133 = "period133"; //33:00
        public const string period134 = "period134"; //33:15
        public const string period135 = "period135"; //33:30
        public const string period136 = "period136"; //33:45
        public const string period137 = "period137"; //34:00
        public const string period138 = "period138"; //34:15
        public const string period139 = "period139"; //34:30
        public const string period140 = "period140"; //34:45
        public const string period141 = "period141"; //35:00
        public const string period142 = "period142"; //35:15
        public const string period143 = "period143"; //35:30
        public const string period144 = "period144"; //35:45
        public const string group_id = "group_id"; //グループID
        public const string group_name = "group_name"; //グループ名
        public const string year_month_day = "year_month_day"; //年月日
        public const string role_code = "role_code"; //役割コード
        public const string necessary_num = "necessary_num"; //必要人数
        public const string date_category_cd = "date_category_cd"; //日付カテゴリコード
        public const string date_category_name = "date_category_name"; //日付カテゴリ名称
        public const string import_days = "import_days"; //インポート日数
        public const string fixed_columns_count = "fixed_columns_count"; //固定列数
        public const string period_count = "period_count"; //コマ数
        public const string dep_flag = "dep_flag"; //部署フラグ
        public const string plan_result = "plan_result"; //種別
        public const string plan_result_color = "plan_result_color"; //種別背景色
        public const string period1_color = "period1_color"; //コマ1背景色
        public const string period2_color = "period2_color"; //コマ2背景色
        public const string period3_color = "period3_color"; //コマ3背景色
        public const string period4_color = "period4_color"; //コマ4背景色
        public const string period5_color = "period5_color"; //コマ5背景色
        public const string period6_color = "period6_color"; //コマ6背景色
        public const string period7_color = "period7_color"; //コマ7背景色
        public const string period8_color = "period8_color"; //コマ8背景色
        public const string period9_color = "period9_color"; //コマ9背景色
        public const string period10_color = "period10_color"; //コマ10背景色
        public const string period11_color = "period11_color"; //コマ11背景色
        public const string period12_color = "period12_color"; //コマ12背景色
        public const string period13_color = "period13_color"; //コマ13背景色
        public const string period14_color = "period14_color"; //コマ14背景色
        public const string period15_color = "period15_color"; //コマ15背景色
        public const string period16_color = "period16_color"; //コマ16背景色
        public const string period17_color = "period17_color"; //コマ17背景色
        public const string period18_color = "period18_color"; //コマ18背景色
        public const string period19_color = "period19_color"; //コマ19背景色
        public const string period20_color = "period20_color"; //コマ20背景色
        public const string period21_color = "period21_color"; //コマ21背景色
        public const string period22_color = "period22_color"; //コマ22背景色
        public const string period23_color = "period23_color"; //コマ23背景色
        public const string period24_color = "period24_color"; //コマ24背景色
        public const string period25_color = "period25_color"; //コマ25背景色
        public const string period26_color = "period26_color"; //コマ26背景色
        public const string period27_color = "period27_color"; //コマ27背景色
        public const string period28_color = "period28_color"; //コマ28背景色
        public const string period29_color = "period29_color"; //コマ29背景色
        public const string period30_color = "period30_color"; //コマ30背景色
        public const string period31_color = "period31_color"; //コマ31背景色
        public const string period32_color = "period32_color"; //コマ32背景色
        public const string period33_color = "period33_color"; //コマ33背景色
        public const string period34_color = "period34_color"; //コマ34背景色
        public const string period35_color = "period35_color"; //コマ35背景色
        public const string period36_color = "period36_color"; //コマ36背景色
        public const string period37_color = "period37_color"; //コマ37背景色
        public const string period38_color = "period38_color"; //コマ38背景色
        public const string period39_color = "period39_color"; //コマ39背景色
        public const string period40_color = "period40_color"; //コマ40背景色
        public const string period41_color = "period41_color"; //コマ41背景色
        public const string period42_color = "period42_color"; //コマ42背景色
        public const string period43_color = "period43_color"; //コマ43背景色
        public const string period44_color = "period44_color"; //コマ44背景色
        public const string period45_color = "period45_color"; //コマ45背景色
        public const string period46_color = "period46_color"; //コマ46背景色
        public const string period47_color = "period47_color"; //コマ47背景色
        public const string period48_color = "period48_color"; //コマ48背景色
        public const string period49_color = "period49_color"; //コマ49背景色
        public const string period50_color = "period50_color"; //コマ50背景色
        public const string period51_color = "period51_color"; //コマ51背景色
        public const string period52_color = "period52_color"; //コマ52背景色
        public const string period53_color = "period53_color"; //コマ53背景色
        public const string period54_color = "period54_color"; //コマ54背景色
        public const string period55_color = "period55_color"; //コマ55背景色
        public const string period56_color = "period56_color"; //コマ56背景色
        public const string period57_color = "period57_color"; //コマ57背景色
        public const string period58_color = "period58_color"; //コマ58背景色
        public const string period59_color = "period59_color"; //コマ59背景色
        public const string period60_color = "period60_color"; //コマ60背景色
        public const string period61_color = "period61_color"; //コマ61背景色
        public const string period62_color = "period62_color"; //コマ62背景色
        public const string period63_color = "period63_color"; //コマ63背景色
        public const string period64_color = "period64_color"; //コマ64背景色
        public const string period65_color = "period65_color"; //コマ65背景色
        public const string period66_color = "period66_color"; //コマ66背景色
        public const string period67_color = "period67_color"; //コマ67背景色
        public const string period68_color = "period68_color"; //コマ68背景色
        public const string period69_color = "period69_color"; //コマ69背景色
        public const string period70_color = "period70_color"; //コマ70背景色
        public const string period71_color = "period71_color"; //コマ71背景色
        public const string period72_color = "period72_color"; //コマ72背景色
        public const string period73_color = "period73_color"; //コマ73背景色
        public const string period74_color = "period74_color"; //コマ74背景色
        public const string period75_color = "period75_color"; //コマ75背景色
        public const string period76_color = "period76_color"; //コマ76背景色
        public const string period77_color = "period77_color"; //コマ77背景色
        public const string period78_color = "period78_color"; //コマ78背景色
        public const string period79_color = "period79_color"; //コマ79背景色
        public const string period80_color = "period80_color"; //コマ80背景色
        public const string period81_color = "period81_color"; //コマ81背景色
        public const string period82_color = "period82_color"; //コマ82背景色
        public const string period83_color = "period83_color"; //コマ83背景色
        public const string period84_color = "period84_color"; //コマ84背景色
        public const string period85_color = "period85_color"; //コマ85背景色
        public const string period86_color = "period86_color"; //コマ86背景色
        public const string period87_color = "period87_color"; //コマ87背景色
        public const string period88_color = "period88_color"; //コマ88背景色
        public const string period89_color = "period89_color"; //コマ89背景色
        public const string period90_color = "period90_color"; //コマ90背景色
        public const string period91_color = "period91_color"; //コマ91背景色
        public const string period92_color = "period92_color"; //コマ92背景色
        public const string period93_color = "period93_color"; //コマ93背景色
        public const string period94_color = "period94_color"; //コマ94背景色
        public const string period95_color = "period95_color"; //コマ95背景色
        public const string period96_color = "period96_color"; //コマ96背景色
        public const string period97_color = "period97_color"; //コマ97背景色
        public const string period98_color = "period98_color"; //コマ98背景色
        public const string period99_color = "period99_color"; //コマ99背景色
        public const string period100_color = "period100_color"; //コマ100背景色
        public const string period101_color = "period101_color"; //コマ101背景色
        public const string period102_color = "period102_color"; //コマ102背景色
        public const string period103_color = "period103_color"; //コマ103背景色
        public const string period104_color = "period104_color"; //コマ104背景色
        public const string period105_color = "period105_color"; //コマ105背景色
        public const string period106_color = "period106_color"; //コマ106背景色
        public const string period107_color = "period107_color"; //コマ107背景色
        public const string period108_color = "period108_color"; //コマ108背景色
        public const string period109_color = "period109_color"; //コマ109背景色
        public const string period110_color = "period110_color"; //コマ110背景色
        public const string period111_color = "period111_color"; //コマ111背景色
        public const string period112_color = "period112_color"; //コマ112背景色
        public const string period113_color = "period113_color"; //コマ113背景色
        public const string period114_color = "period114_color"; //コマ114背景色
        public const string period115_color = "period115_color"; //コマ115背景色
        public const string period116_color = "period116_color"; //コマ116背景色
        public const string period117_color = "period117_color"; //コマ117背景色
        public const string period118_color = "period118_color"; //コマ118背景色
        public const string period119_color = "period119_color"; //コマ119背景色
        public const string period120_color = "period120_color"; //コマ120背景色
        public const string period121_color = "period121_color"; //コマ121背景色
        public const string period122_color = "period122_color"; //コマ122背景色
        public const string period123_color = "period123_color"; //コマ123背景色
        public const string period124_color = "period124_color"; //コマ124背景色
        public const string period125_color = "period125_color"; //コマ125背景色
        public const string period126_color = "period126_color"; //コマ126背景色
        public const string period127_color = "period127_color"; //コマ127背景色
        public const string period128_color = "period128_color"; //コマ128背景色
        public const string period129_color = "period129_color"; //コマ129背景色
        public const string period130_color = "period130_color"; //コマ130背景色
        public const string period131_color = "period131_color"; //コマ131背景色
        public const string period132_color = "period132_color"; //コマ132背景色
        public const string period133_color = "period133_color"; //コマ133背景色
        public const string period134_color = "period134_color"; //コマ134背景色
        public const string period135_color = "period135_color"; //コマ135背景色
        public const string period136_color = "period136_color"; //コマ136背景色
        public const string period137_color = "period137_color"; //コマ137背景色
        public const string period138_color = "period138_color"; //コマ138背景色
        public const string period139_color = "period139_color"; //コマ139背景色
        public const string period140_color = "period140_color"; //コマ140背景色
        public const string period141_color = "period141_color"; //コマ141背景色
        public const string period142_color = "period142_color"; //コマ142背景色
        public const string period143_color = "period143_color"; //コマ143背景色
        public const string period144_color = "period144_color"; //コマ144背景色
        public const string employee_count = "employee_count"; //従業員数
        public const string item = "item"; //項目
        public const string start_period = "start_period"; //開始コマ
        public const string end_period = "end_period"; //終了コマ
        public const string break_start_period = "break_start_period"; //休憩開始コマ
        public const string break_end_period = "break_end_period"; //休憩終了コマ
        public const string break0_start_period = "break0_start_period"; //休憩1開始コマ
        public const string break0_end_period = "break0_end_period"; //休憩1終了コマ
        public const string break1_start_period = "break1_start_period"; //休憩2開始コマ
        public const string break1_end_period = "break1_end_period"; //休憩2終了コマ
        public const string break2_start_period = "break2_start_period"; //休憩3開始コマ
        public const string break2_end_period = "break2_end_period"; //休憩3終了コマ
        public const string break3_start_period = "break3_start_period"; //休憩4開始コマ
        public const string break3_end_period = "break3_end_period"; //休憩4終了コマ
        public const string break4_start_period = "break4_start_period"; //休憩5開始コマ
        public const string break4_end_period = "break4_end_period"; //休憩5終了コマ
        public const string status_code = "status_code"; //ステータスコード
        public const string group_abbreviation = "group_abbreviation"; //グループ略号
        public const string group_color = "group_color"; //グループ色
        public const string indirect_name = "indirect_name"; //間接名
        public const string indirect_color = "indirect_color"; //間接作業色
        public const string shift_name = "shift_name"; //シフト名
        public const string last_access_date = "last_access_date"; //最終アクセス日時
        public const string authority_name = "authority_name"; //権限
        public const string dep_name = "dep_name"; //部署名
        public const string main_group_name = "main_group_name"; //メイングループ名
        public const string com_name = "com_name"; //会社名
        public const string sufficiency_rate = "sufficiency_rate"; //充足率
        public const string posted_emp_rate = "posted_emp_rate"; //配置人数比率
        public const string sufficiency_over_rate = "sufficiency_over_rate"; //過充足率
        public const string hope_rate = "hope_rate"; //希望通過率
        public const string shift_type_code = "shift_type_code"; //シフト種別コード
        public const string disp_start_period = "disp_start_period"; //表示開始日時
        public const string disp_end_period = "disp_end_period"; //表示終了日時
        public const string disp_break_start_period = "disp_break_start_period"; //表示休憩開始日時
        public const string disp_break_end_period = "disp_break_end_period"; //表示休憩終了日時
        public const string disp_break0_start_period = "disp_break0_start_period"; //表示休憩1開始時間
        public const string disp_break0_end_period = "disp_break0_end_period"; //表示休憩1終了時間
        public const string disp_break1_start_period = "disp_break1_start_period"; //表示休憩2開始時間
        public const string disp_break1_end_period = "disp_break1_end_period"; //表示休憩2終了時間
        public const string disp_break2_start_period = "disp_break2_start_period"; //表示休憩3開始時間
        public const string disp_break2_end_period = "disp_break2_end_period"; //表示休憩3終了時間
        public const string disp_break3_start_period = "disp_break3_start_period"; //表示休憩4開始時間
        public const string disp_break3_end_period = "disp_break3_end_period"; //表示休憩4終了時間
        public const string disp_break4_start_period = "disp_break4_start_period"; //表示休憩5開始時間
        public const string disp_break4_end_period = "disp_break4_end_period"; //表示休憩5終了時間
        public const string work_minutes = "work_minutes"; //勤務時間
        public const string break_minutes = "break_minutes"; //休憩時間
        public const string hope_code = "hope_code"; //希望コード
        public const string full_fledged_employee = "full_fledged_employee"; //正社員
        public const string contract_worker = "contract_worker"; //契約社員
        public const string part_timer = "part_timer"; //パート
        public const string arbeit = "arbeit"; //アルバイト
        public const string item_name = "item_name"; //項目名
        public const string total = "total"; //合計
        public const string last_year_same_period = "last_year_same_period"; //前年同期間
        public const string posted_emp_num = "posted_emp_num"; //配置人数
        public const string rate = "rate"; //割合
        public const string manpower_cost = "manpower_cost"; //人件費
        public const string man_unit = "man_unit"; //人
        public const string skill_code = "skill_code"; //スキルコード
        public const string skill_level = "skill_level"; //スキルレベル
        public const string service_level = "service_level"; //サービスレベル
        public const string dispatch_worker = "dispatch_worker"; //派遣社員
        public const string day1_shift_code = "day1_shift_code"; //1日のシフトコード
        public const string day2_shift_code = "day2_shift_code"; //2日のシフトコード
        public const string day3_shift_code = "day3_shift_code"; //3日のシフトコード
        public const string day4_shift_code = "day4_shift_code"; //4日のシフトコード
        public const string day5_shift_code = "day5_shift_code"; //5日のシフトコード
        public const string day6_shift_code = "day6_shift_code"; //6日のシフトコード
        public const string day7_shift_code = "day7_shift_code"; //7日のシフトコード
        public const string day8_shift_code = "day8_shift_code"; //8日のシフトコード
        public const string day9_shift_code = "day9_shift_code"; //9日のシフトコード
        public const string day10_shift_code = "day10_shift_code"; //10日のシフトコード
        public const string day11_shift_code = "day11_shift_code"; //11日のシフトコード
        public const string day12_shift_code = "day12_shift_code"; //12日のシフトコード
        public const string day13_shift_code = "day13_shift_code"; //13日のシフトコード
        public const string day14_shift_code = "day14_shift_code"; //14日のシフトコード
        public const string day15_shift_code = "day15_shift_code"; //15日のシフトコード
        public const string day16_shift_code = "day16_shift_code"; //16日のシフトコード
        public const string day17_shift_code = "day17_shift_code"; //17日のシフトコード
        public const string day18_shift_code = "day18_shift_code"; //18日のシフトコード
        public const string day19_shift_code = "day19_shift_code"; //19日のシフトコード
        public const string day20_shift_code = "day20_shift_code"; //20日のシフトコード
        public const string day21_shift_code = "day21_shift_code"; //21日のシフトコード
        public const string day22_shift_code = "day22_shift_code"; //22日のシフトコード
        public const string day23_shift_code = "day23_shift_code"; //23日のシフトコード
        public const string day24_shift_code = "day24_shift_code"; //24日のシフトコード
        public const string day25_shift_code = "day25_shift_code"; //25日のシフトコード
        public const string day26_shift_code = "day26_shift_code"; //26日のシフトコード
        public const string day27_shift_code = "day27_shift_code"; //27日のシフトコード
        public const string day28_shift_code = "day28_shift_code"; //28日のシフトコード
        public const string day29_shift_code = "day29_shift_code"; //29日のシフトコード
        public const string day30_shift_code = "day30_shift_code"; //30日のシフトコード
        public const string day31_shift_code = "day31_shift_code"; //31日のシフトコード
        public const string obj_func_code = "obj_func_code"; //目的関数コード
        public const string target_code = "target_code"; //比較対象コード
        public const string correl = "correl"; //相関係数
        public const string preproc_sec = "preproc_sec"; //前処理時間（秒）
        public const string base_month = "base_month"; //基準月
        public const string shift_type_name = "shift_type_name"; //シフト種別名
        public const string average = "average"; //平均
        public const string hope_name = "hope_name"; //希望
        public const string work_num = "work_num"; //出勤数
        public const string work_time = "work_time"; //勤務時間
        public const string work_time_stress = "work_time_stress"; //勤務時間ストレス
        public const string several_working_num = "several_working_num"; //{0}連勤数
        public const string ok_work_num = "ok_work_num"; //○出勤回数
        public const string no_work_num = "no_work_num"; //△出勤回数
        public const string sub_work_num = "sub_work_num"; //サブ勤務回数
        public const string cost = "cost"; //コスト
        public const string sub_group_name = "sub_group_name"; //サブグループ名
        public const string role_name = "role_name"; //役割名
        public const string node_flag = "node_flag"; //ノードフラグ
        public const string main = "main"; //メイン
        public const string sub = "sub"; //サブ
        public const string skill_name = "skill_name"; //スキル名
        public const string zip_code = "zip_code"; //郵便番号
        public const string address = "address"; //住所
        public const string tel_number = "tel_number"; //電話番号
        public const string fax_number = "fax_number"; //FAX番号
        public const string sub_group_id = "sub_group_id"; //サブグループID
        public const string shift_num = "shift_num"; //シフト数
        public const string holiday_num = "holiday_num"; //休日数
        public const string parent_id = "parent_id"; //親部署コード
        public const string member_of = "member_of"; //所属グループ
        public const string start_time = "start_time"; //開始時間
        public const string end_time = "end_time"; //終了時間
        public const string break0_start_time = "break0_start_time"; //休憩1開始時間
        public const string break0_end_time = "break0_end_time"; //休憩1終了時間
        public const string break1_start_time = "break1_start_time"; //休憩2開始時間
        public const string break1_end_time = "break1_end_time"; //休憩2終了時間
        public const string break2_start_time = "break2_start_time"; //休憩3開始時間
        public const string break2_end_time = "break2_end_time"; //休憩3終了時間
        public const string break3_start_time = "break3_start_time"; //休憩4開始時間
        public const string break3_end_time = "break3_end_time"; //休憩4終了時間
        public const string break4_start_time = "break4_start_time"; //休憩5開始時間
        public const string break4_end_time = "break4_end_time"; //休憩5終了時間
        public const string sent_flg = "sent_flg"; //配信フラグ
        public const string tmp_start_date = "tmp_start_date"; //開始月
        public const string tmp_end_date = "tmp_end_date"; //終了月
        public const string working_minutes = "working_minutes"; //労働時間
        public const string over_minutes = "over_minutes"; //残業時間
        public const string work_cost = "work_cost"; //労働コスト
        public const string rescheduling_flg = "rescheduling_flg"; //リスケジューリングフラグ
        public const string result_last_date = "result_last_date"; //実績利用最終日
        public const string constraint_type = "constraint_type"; //制約種別
        public const string year_month = "year_month"; //年月
        public const string indent_group = "indent_group"; //インデントグループ
        public const string indent_dep = "indent_dep"; //インデント部署
        public const string holiday = "holiday"; //祝日
        public const string target_day = "target_day"; //対象日
        public const string condition = "condition"; //条件
        public const string emp_constraint_code = "emp_constraint_code"; //従業員制約コード
        public const string category_code = "category_code"; //従業員制約要素のカテゴリコード
        public const string seq = "seq"; //連番
        public const string break0_status_code = "break0_status_code"; //休憩1ステータスコード
        public const string break1_status_code = "break1_status_code"; //休憩2ステータスコード
        public const string break2_status_code = "break2_status_code"; //休憩3ステータスコード
        public const string break3_status_code = "break3_status_code"; //休憩4ステータスコード
        public const string break4_status_code = "break4_status_code"; //休憩5ステータスコード
        public const string priority = "priority"; //優先順位
        public const string col_type_code = "col_type_code"; //列種別コード
        public const string layout_code = "layout_code"; //レイアウトコード
        public const string layout_name = "layout_name"; //レイアウト名称
        public const string panel1_code = "panel1_code"; //パネル1コード
        public const string panel2_code = "panel2_code"; //パネル2コード
        public const string panel3_code = "panel3_code"; //パネル3コード
        public const string panel4_code = "panel4_code"; //パネル4コード
        public const string panel5_code = "panel5_code"; //パネル5コード
        public const string panel_code = "panel_code"; //パネルコード
        public const string panel_name = "panel_name"; //パネル名称
        public const string horizontal_axis_code = "horizontal_axis_code"; //横軸コード
        public const string vertical_axis_code = "vertical_axis_code"; //縦軸コード
        public const string graph_type_code = "graph_type_code"; //グラフ種別コード
        #endregion

        /// <summary>メッセージID</summary>
        #region MessageId
        public const string MSG_LOGIN_FAILED = "MSG_LOGIN_FAILED"; //ログインに失敗しました。ログインID、またはパスワードが異なります。"
        public const string MSG_CRITICAL_ERROR = "MSG_CRITICAL_ERROR"; //処理を続行できないため強制終了します。再ログインしてください。
        public const string MSG_MULTIPLE_START_ERROR = "MSG_MULTIPLE_START_ERROR"; //多重起動できません。
        public const string CONSTRAINT_TYPE_HARD = "CONSTRAINT_TYPE_HARD"; //絶対制約
        public const string CONSTRAINT_TYPE_SOFT = "CONSTRAINT_TYPE_SOFT"; //考慮制約
        public const string MSG_MAX_ROW = "MSG_MAX_ROW"; //検索結果が{0}件を超えているため結果を表示できません。検索条件を変更してください。
        public const string SNAP_SEC = "SNAP_SEC"; //秒
        public const string SNAP_MIN = "SNAP_MIN"; //分
        public const string SNAP_HOUR = "SNAP_HOUR"; //時間
        public const string AUTH_TYPE_ADMIN = "AUTH_TYPE_ADMIN"; //権限：システム管理者
        public const string AUTH_TYPE_PLANNER = "AUTH_TYPE_PLANNER"; //権限：計画者
        public const string AUTH_TYPE_WORKER = "AUTH_TYPE_WORKER"; //権限：作業者
        public const string MSG_REMAIN_ERROR = "MSG_REMAIN_ERROR"; //入力値にエラーがあります。エラー部分にマウスカーソルを当てると内容を確認できます。"
        public const string MSG_DUPLICATE = "MSG_DUPLICATE"; //{0}が重複しています。
        public const string MSG_NOT_SELECTED = "MSG_NOT_SELECTED"; //未選択
        public const string MSG_RESERVED = "MSG_RESERVED"; //{0}は、システムで利用するため設定できません。
        public const string MSG_NOT_ENTRY = "MSG_NOT_ENTRY"; //未入力
        public const string MSG_BETTER = "MSG_BETTER"; //奨励パターン
        public const string MSG_WORSE = "MSG_WORSE"; //非奨励パターン
        public const string MSG_FIXED = "MSG_FIXED"; //固定パターン
        public const string MSG_BANNED = "MSG_BANNED"; //禁止パターン
        public const string GRID_HEADER_NECESSARY_NUM = "GRID_HEADER_NECESSARY_NUM"; //必要人数
        public const string LIST_ITEM_ALL_GROUP = "LIST_ITEM_ALL_GROUP"; //全グループ
        public const string MSG_SHIFT_PTN_ERROR = "MSG_SHIFT_PTN_ERROR"; //無効なシフトパターンです。
        public const string MSG_CANNOT_SORT = "MSG_CANNOT_SORT"; //編集中はソートできません。
        public const string MSG_FILE_NO_EXIST = "MSG_FILE_NO_EXIST"; //対象のファイルが存在しません。
        public const string FILE_DIALOG_FILTER_CSV = "FILE_DIALOG_FILTER_CSV"; //CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*
        public const string MSFILE_DIALOG_TITLE_READ_CSV = "MSFILE_DIALOG_TITLE_READ_CSV"; //CSV取込
        public const string MSG_CSV_FORMAT_ERROR = "MSG_CSV_FORMAT_ERROR"; //CSVファイルの形式が不正です。
        public const string MSG_NO_INPORT_DATA_ERROR = "MSG_NO_INPORT_DATA_ERROR"; //取込対象データが存在しません。
        public const string PR_TYPE_PLAN = "PR_TYPE_PLAN"; //計画
        public const string PR_TYPE_RESULT = "PR_TYPE_RESULT"; //実績
        public const string WORK_STATUS_BREAK = "WORK_STATUS_BREAK"; //休憩
        public const string MSG_LOCKED_ADMIN_MENU = "MSG_LOCKED_ADMIN_MENU"; //{0}さんが管理者メニューを利用中です。
        public const string MSG_DATE_ERROR = "MSG_DATE_ERROR"; //日付の値が不正です。
        public const string MSG_NOT_ENTRY_EXIST = "MSG_NOT_ENTRY_EXIST"; //未入力の項目が存在します。
        public const string MONDAY = "MONDAY"; //月
        public const string TUESDAY = "TUESDAY"; //火
        public const string WEDNESDAY = "WEDNESDAY"; //水
        public const string THURSDAY = "THURSDAY"; //木
        public const string FRIDAY = "FRIDAY"; //金
        public const string SATURDAY = "SATURDAY"; //土
        public const string SUNDAY = "SUNDAY"; //日
        public const string LOG_CONTENT = "LOG_CONTENT"; //【内容】
        public const string LOG_DURATION = "LOG_DURATION"; //【期間】
        public const string LOG_TARGET_DAY = "LOG_TARGET_DAY"; //【対象日】
        public const string MST_EDITING = "MST_EDITING"; //編集中
        public const string UPD_MSG_ACT = "UPD_MSG_ACT"; //{0}の更新
        public const string UPD_SUCCESS = "UPD_SUCCESS"; //成功
        public const string UPD_FAILURE = "UPD_FAILURE"; //失敗
        public const string MSG_NO_FUNC_ERROR = "MSG_NO_FUNC_ERROR"; //目的関数が設定されていません。
        public const string MSG_SNAPSHOT_ALREADY_DELETED = "MSG_SNAPSHOT_ALREADY_DELETED"; //指定されたスナップショットは、別のユーザによって削除されています。
        public const string MSG_UNSOLVE = "MSG_UNSOLVE"; //制約条件と必要人数を見直してください。
        public const string ACT_PRE_PROCESS = "ACT_PRE_PROCESS"; //前処理
        public const string MSG_PRE_PROC_FAIL = "MSG_PRE_PROC_FAIL"; //前処理に失敗しました。
        public const string DATE_FORMAT_YYYYMM = "DATE_FORMAT_YYYYMM"; //yyyy/MM
        public const string CTL_CLOSE = "CTL_CLOSE"; //閉じる
        public const string LBL_MINUTES = "LBL_MINUTES"; //{0}分
        public const string LBL_PERCENT = "LBL_PERCENT"; //{0}%
        public const string LBL_DURATION = "LBL_DURATION"; //{0}～{1}
        public const string GRID_CAP_SUM = "GRID_CAP_SUM"; //合計
        public const string DEFAULT_SNAPSHOT_NAME = "DEFAULT_SNAPSHOT_NAME"; //スナップショット{0} yyyy-MM-dd HH:mm
        public const string MSG_MAIN_GROUP_NO_CHECKED = "MSG_MAIN_GROUP_NO_CHECKED"; //メイングループを選択してください。
        public const string MSG_NOT_SELECTED_ROLE = "MSG_NOT_SELECTED_ROLE"; //役割を選択してください。
        public const string LIST_ITEM_ALL_ROLE = "LIST_ITEM_ALL_ROLE"; //全役割
        public const string FORM_COM_SELECTION = "FORM_COM_SELECTION"; //会社選択
        public const string MSG_ERROR_VALUE = "MSG_ERROR_VALUE"; //設定できない値が含まれています。
        public const string MSG_ERROR_GROUP_UPDATE = "MSG_ERROR_GROUP_UPDATE"; //全従業員にメイングループを割り当ててください。
        public const string NEW_GROUP_NAME = "NEW_GROUP_NAME"; //新しいグループ
        public const string MSG_ERROR_MAIN_GROUP = "MSG_ERROR_MAIN_GROUP"; //既に他のグループがメインに割り当てられています。
        public const string DATE_MASK_HHMM = "DATE_MASK_HHMM"; //00:00
        public const string DATE_FORMAT_HHMM = "DATE_FORMAT_HHMM"; //HH:mm
        public const string MSG_SHIFT_NO_SERIES = "MSG_SHIFT_NO_SERIES"; //シフトは連続した時間で設定してください。
        public const string MSG_WRONG_SHIFT = "MSG_WRONG_SHIFT"; //不正なシフトです。
        public const string MSG_BREAK_NO_SERIES = "MSG_BREAK_NO_SERIES"; //休憩は連続した時間で設定してください。
        public const string MSG_TRANSMITED = "MSG_TRANSMITED"; //配信しました。
        public const string MSG_SENT_ERROR = "MSG_SENT_ERROR"; //配信済のため取込めません。
        public const string MSG_NON_AUTHORITH = "MSG_NON_AUTHORITH"; //ログインに失敗しました。権限がありません
        public const string MSG_PAST_ERROR = "MSG_PAST_ERROR"; //現在日時以前のスケジュールを含むスナップショットは配信できません。
        public const string MSG_DURATION_ERROR = "MSG_DURATION_ERROR"; //配信済みスケジュールを変更するには同期間のスナップショットを選択してください。
        public const string MSG_USING_CODE = "MSG_USING_CODE"; //現在使用中のコードです。
        public const string MSG_SNAPSHOT_LIMIT_OVER = "MSG_SNAPSHOT_LIMIT_OVER"; //スナップショット数が上限を越えています。
        public const string MSG_SCHEDULING_ERROR = "MSG_SCHEDULING_ERROR"; //スケジューリング中にエラーが発生しました。システム管理者に問い合わせてください。
        public const string ACT_POST_PROCESS = "ACT_POST_PROCESS"; //後処理
        public const string MSG_POST_PROC_FAIL = "MSG_POST_PROC_FAIL"; //後処理に失敗しました。
        public const string MSG_ERROR_FIX_PLAN_DAYS = "MSG_ERROR_FIX_PLAN_DAYS"; //確定計画日数が上限を超えています。
        public const string MSG_FUNC_SERVLEVEL_ONLY = "MSG_FUNC_SERVLEVEL_ONLY"; //目的関数サービスレベルは単独では使用できません。
        public const string MSG_DUPLICATE_CALENDAR = "MSG_DUPLICATE_CALENDAR"; //日付カテゴリと年月日が重複しています。
        public const string HOLIDAY = "HOLIDAY"; //祝
        public const string CONDITION_XML_FORMAT = "CONDITION_XML_FORMAT"; //<emp_constr_condition count="1" code="0" />
        public const string TOOLTIP_EMP_CONSTR_DIALOG = "TOOLTIP_EMP_CONSTR_DIALOG"; //<b><parm>対象</parm></b>
        public const string CONDITION_UNIT_COUNT = "CONDITION_UNIT_COUNT"; //回
        public const string CONDITION_UNIT_HOUR = "CONDITION_UNIT_HOUR"; //時間
        public const string LBL_MONTH = "LBL_MONTH"; //月
        public const string LBL_WEEK = "LBL_WEEK"; //週
        public const string MSG_SAVE_FOUND_SOLUTION = "MSG_SAVE_FOUND_SOLUTION"; //既に見つかっている暫定解を保存しますか？
        public const string MSG_NOT_EXISTS_SHIFT_DETAIL = "MSG_NOT_EXISTS_SHIFT_DETAIL"; //シフト詳細情報が設定されていません。
        public const string MSG_BREAK_LIMIT_OVER = "MSG_BREAK_LIMIT_OVER"; //休憩は最大{0}回までしか設定できません。
        public const string MSG_GROUP_LIMIT_OVER = "MSG_GROUP_LIMIT_OVER"; //勤務対象グループは最大{0}グループまでしか設定できません。
        public const string MSG_FIX_FIX_SHIFT_PTN_ERROR = "MSG_FIX_FIX_SHIFT_PTN_ERROR"; //矛盾する固定シフトパターンを設定しています。
        public const string MSG_FIX_BAN_SHIFT_PTN_ERROR = "MSG_FIX_BAN_SHIFT_PTN_ERROR"; //同一のシフトパターンを固定かつ禁止しています。
        public const string MSG_CONFLICTED_SHIFT_PTN_ERROR = "MSG_CONFLICTED_SHIFT_PTN_ERROR"; //制約追加時に制約矛盾が発生しました。エラー部分にカーソルを当てると内容を確認できます
        public const string MSG_CONSTRAINT_CONTRADICTION = "MSG_CONSTRAINT_CONTRADICTION"; //制約条件が矛盾しています。
        public const string MSG_DELETE_NON_UPDATE_DATA = "MSG_DELETE_NON_UPDATE_DATA"; //更新していない内容は破棄されますがよろしいですか？
        public const string FILE_DIALOG_FILTER = "FILE_DIALOG_FILTER"; //対応ファイル形式(*.xlsx, *.xlsm, *.xls, *.csv)|*.xlsx;*.xlsm;*.xls;*.csv|すべてのファイル(*.*)|*.*
        public const string MSFILE_DIALOG_TITLE_READ = "MSFILE_DIALOG_TITLE_READ"; //ファイル取込
        public const string MSG_FILE_FORMAT_ERROR = "MSG_FILE_FORMAT_ERROR"; //ファイルの形式が不正です。
        public const string GFILE_DIALOG_FILTER = "GFILE_DIALOG_FILTER"; //対応ファイル形式(*.xlsx, *.xlsm, *.xls, *.csv)|*.xlsx;*.xlsm;*.xls;*.csv|すべてのファイル(*.*)|*.*
        public const string FILE_DIALOG_TITLE = "FILE_DIALOG_TITLE"; //ファイル取込

        #endregion

        /// <summary>共通設定</summary>
        #region 共通設定
        public const string EmpNumCsvPath = "EmpNumCsvPath"; //必要人数をインポートするCSVのパス
        public const string MaxRowCount = "MaxRowCount"; //検索時の最大取得行数
        public const string MaxAppendRowCount = "MaxAppendRowCount"; //新規追加行ダイアログ
        public const string MaxImportDays = "MaxImportDays"; //必要人数をインポートする日数の最大値
        public const string WorkPlanColor = "WorkPlanColor"; //勤務予実照会の計画カラー
        public const string WorkResultColor = "WorkResultColor"; //勤務予実照会の実績カラー
        public const string IndirectColor = "IndirectColor"; //勤務予実照会の間接作業カラー
        public const string BreakColor = "BreakColor"; //勤務予実照会の休憩カラー
        public const string PaidBreakColor = "PaidBreakColor"; //シフト詳細グリッドの有給休憩コマのカラー
        public const string UpdateHolidaysSchedule = "UpdateHolidaysSchedule"; //祝日情報更新日スケジュール（"dd HH:mm"形式で記述）
        public const string PeriodNumber = "PeriodNumber"; //コマ数
        public const string MinMonthWorkDays = "MinMonthWorkDays"; //月間最小勤務日数
        public const string MaxMonthWorkDays = "MaxMonthWorkDays"; //月間最大勤務日数
        public const string ConsecutiveWorkDays = "ConsecutiveWorkDays"; //最大連続勤務日数
        public const string MaxWeekWorkHours = "MaxWeekWorkHours"; //週間最大労働時間
        public const string SupportMaxWeekWorkHours = "SupportMaxWeekWorkHours"; //週間最大労働時間（社会保険適用外）
        public const string MinWeekWorkDays = "MinWeekWorkDays"; //週間最小勤務日数
        public const string MaxWeekWorkDays = "MaxWeekWorkDays"; //週間最大勤務日数
        public const string OnePeriodMinutes = "OnePeriodMinutes"; //1コマ当たりの時間（分）
        public const string ClosingDate = "ClosingDate"; //従業員希望の締めの日付（yyyy/MM形式で記述）
        public const string SessionTimeOutMinutes = "SessionTimeOutMinutes"; //セッションタイムアウト時間（分）
        public const string SweepSchedule = "SweepSchedule"; //テンポラリファイル削除処理スケジュール（HH:mm形式で記述）
        public const string TmpRetentionDay = "TmpRetentionDay"; //Tmpフォルダ内のファイルの保持日数
        public const string MaxSubGroupNum = "MaxSubGroupNum"; //従業員に割り当てられるサブグループ数
        public const string MaxSkillLevel = "MaxSkillLevel"; //従業員のスキルレベルの最大値
        public const string RootGroupId = "RootGroupId"; //ルートグループID
        public const string DeleteGroupId = "DeleteGroupId"; //削除グループのID
        public const string TodayColor = "TodayColor"; //グリッドヘッダの本日の背景色
        public const string MultiObjWeightCost = "MultiObjWeightCost"; //目的関数コストのウエイト
        public const string MultiObjWeightSatis = "MultiObjWeightSatis"; //目的関数従業員満足度のウエイト
        public const string MultiObjWeightServ = "MultiObjWeightServ"; //目的関数サービスレベルのウエイト
        public const string WFMOptimizerPath = "WFMOptimizerPath"; //WFMOptimizerの実行ファイルのパス
        public const string OKGap = "OKGap"; //希望コード・OKに割付いたときのギャップ
        public const string NonHopeGap = "NonHopeGap"; //希望なしのシフトに割付いたときのギャップ
        public const string NOGap = "NOGap"; //希望コード・NOに割付いたときのギャップ
        public const string WorkNumGap = "WorkNumGap"; //従業員満足度最大化の勤務日数のギャップ
        public const string SeveralWorkGap = "SeveralWorkGap"; //従業員満足度最大化のX連勤のギャップ
        public const string WorkTimeMedian = "WorkTimeMedian"; //シフトの勤務コマの中央値
        public const string SatisSubGroupGap = "SatisSubGroupGap"; //従業員満足度最大化のサブグループに割付いたときのギャップ
        public const string CostSubGroupGap = "CostSubGroupGap"; //コスト最小化でサブグループに割付いたときのギャップ
        public const string BetterPatternGap = "BetterPatternGap"; //奨励パターンに割付いたときのギャップ
        public const string WorsePatternGap = "WorsePatternGap"; //非奨励パターンに割ついたときのギャップ
        public const string ApfileRepoContext = "ApfileRepoContext"; //APServerからみたDBServer上のWFM用リポジトリパス
        public const string WfmDbBackupSchedule = "WfmDbBackupSchedule"; //WFMDBのバックアップスケジュール（HH:mm形式で記述）
        public const string MaxSnapshotNumber = "MaxSnapshotNumber"; //保持するスナップショットの最大数
        public const string FixShiftRuleCode = "FixShiftRuleCode"; //従業員制約・固定割付のルールコード
        public const string SeveralWorkingNum = "SeveralWorkingNum"; //従業員ストレスとする連勤数
        public const string HitHopeColor = "HitHopeColor"; //希望通りに割り当てられたときの背景色
        public const string MissHopeColor = "MissHopeColor"; //希望から外れたときの背景色
        public const string MaxShiftBreakNumber = "MaxShiftBreakNumber"; //１シフト内の最大休憩回数（SQLは手動で直す必要がある）
        public const string UsedSolverType = "UsedSolverType"; //WFMOptimizerで使用するソルバ名（Gurobi or CBC）
        public const string OriginalShiftAbbreviation = "OriginalShiftAbbreviation"; //シフトマスタに存在しないシフトの略号
        public const string OriginalShiftColor = "OriginalShiftColor"; //シフトマスタに存在しないシフトの色
        public const string EditedShiftPrefix = "EditedShiftPrefix"; //編集されたが開始コマ・終了コマに変更がないシフトの接頭語
        public const string OriginalShiftPrefix = "OriginalShiftPrefix"; //シフトマスタに存在しないシフトの接頭語
        public const string DifferentResultColor = "DifferentResultColor"; //従業員勤務実績紹介で計画と実績のシフトコードが異なる日の色
        public const string ShiftTypeCellColor = "ShiftTypeCellColor"; //シフトパターン制約の項目がシフト種別のセルの背景色
        #endregion

        /// <summary>レイアウトファイル</summary>
        #region LayoutFile
        /// <summary>制約条件入力レイアウトファイル</summary>
        public const string ConstrantLayoutFile = "ConstraintLayout.xml";
        /// <summary>スケジューリング結果表示レイアウトファイル</summary>
        public const string ResultLayoutFile = "ResultLayout.xml";
        /// <summary>スケジューリング結果比較レイアウトファイル</summary>
        public const string ResultCompLayoutFile = "ResultCompLayout.xml";
        /// <summary>スケジューリング結果詳細レイアウトファイル</summary>
        public const string ResultDetailLayoutFile = "ResultDetailLayoutFile.xml";
        #endregion

        /// <summary>パラメータDataTable名</summary>
        #region ParameterTableName
        public const string TableNameTNecessaryEmpNum = "t_necessary_emp_num";
        #endregion

        /// <summary>システム権限</summary>
        #region SystemAuthority
        public const string SystemAuthorityAdmin   = "1";
        public const string SystemAuthorityPlanner = "2";
        public const string SystemAuthorityWorker  = "3";
        #endregion

        /// <summary>シフトパターンカテゴリ</summary>
        #region ShiftPatternCategory
        public const string ShiftPatternCategoryBetter = "Better";
        public const string ShiftPatternCategoryWorse  = "Worse";
        public const string ShiftPatternCategoryFixed  = "Fixed";
        public const string ShiftPatternCategoryBanned = "Banned";
        #endregion

        /// <summary>必要人数設定の表示範囲</summary>
        #region DisplayRange
        public const string DisplayRangeDaily   = "Daily"; //日
        public const string DisplayRangeWeekly  = "Weekly"; //週
        public const string DisplayRangeMonthly = "Monthly"; //月
        #endregion

        /// <summary>削除フラグ</summary>
        #region DeleteFlag
        public const string DeleteFlagDeleted = "1";
        #endregion

        /// <summary>制約種別</summary>
        #region ConstraintType
        public const string BasicConstraint = "1";
        public const string EmpConstraint   = "2";
        #endregion

        /// <summary>従業員制約の条件列XMLの要素</summary>
        public const string EmpConstrConditionElement = "emp_constr_condition";

        /// <summary>制約ルールを格納するXMLの要素</summary>
        #region ConstraintRuleXmlElement
        public const string ConstrConfParamElement  = "constr_conf_param";
        public const string ConstrModelParamElement = "constr_model_param";
        public const string DispStrElement          = "disp_str";
        public const string DispHtmlElement         = "disp_html";
        public const string DispHtmlStrElement      = "disp_html_str";
        public const string EmpConfElement          = "emp_conf";
        public const string ShiftConfElement        = "shift_conf";
        public const string ShiftTypeConfElement    = "shift_type_conf";
        public const string ConstrTypeElement       = "constr_type";
        public const string ConditionConfElement    = "condition_conf";
        #endregion

        /// <summary>制約の種類</summary>
        public enum ConstrType : int {
            Bound = 0,
            Constr,
            Both,
        }

        /// <summary>最適化パラメータ設定を格納するXMLの要素</summary>
        #region OptimizeParamXmlElement
        public const string ObjectiveFunctionElement    = "objective_function";
        public const string MinimizeCostElement         = "minimize_cost";
        public const string MaximizeSatisfactionElement = "maximize_satisfaction";
        public const string MaximizeServiceLevelElement = "maximize_service_level";
        public const string SchedulingPeriodElement     = "scheduling_period";
        public const string StartDateElement            = "start_date";
        public const string EndDateElement              = "end_date";
        public const string OptimizeParamElement        = "optimize_param";
        public const string ExactSolutionElement        = "exact_solution";
        public const string AcceptableErrorsElement     = "acceptable_errors";
        public const string DiscontinuedTimeElement     = "discontinued_time";
        public const string FixPlanDaysElement          = "fix_plan_days";
        #endregion

        /// <summary>XMLの属性</summary>
        #region ConstraintRuleXmlAttrbute
        public const string ValueAttrbute      = "value";
        public const string EnabledAttrbute    = "enabled";
        public const string ImpactAttrbute     = "impact";
        public const string MinutesAttrbute    = "minutes";
        public const string PercentageAttrbute = "percentage";
        public const string DaysAttrbute       = "days";
        public const string CountAttrbute      = "count";
        public const string CodeAttrbute       = "code";
        public const string MultipleAttrbute   = "multiple";
        public const string CanSetRestAttrbute = "can_set_rest";
        public const string UnitAttrbute       = "unit";
        public const string SpanAttrbute       = "span";
        #endregion

        /// <summary>勤務実績ステータス</summary>
        #region WorkResultStatus
        public const string WorkResultStatusWork  = "WORK"; //稼働中
        public const string WorkResultStatusBreak = "BREAK"; //休憩中
        #endregion

        #region DateCategoryCd
        public const string DateCategoryNationalHoliday = "1"; //祝日
        #endregion

        /// <summary>作業予実詳細人数</summary>
        public enum PlanResultDetailItem : int {
            PLAN = 0,   //計画人数
            RESULT,     //実績人数
            BREAK,      //休憩人数
            INDIRECT,   //間接作業人数
            DIFFERENCE  //過不足
        }

        /// <summary>従業員制約の曜日が設定されていないときの値</summary>
        public const string NonDayOfWeekVal = "0000000";

        /// <summary>スナップショットID</summary>
        public const string ShapshotIdConfirm = "0"; //確定スケジュール

        /// <summary>スケジューリング結果詳細の項目</summary>
        public enum DailyResultDetailItem : int {
            NECESSARY = 0,  //必要人数
            PLACE,          //配置人数
            BREAK,          //休憩人数
            DIFFERENCE,     //過不足
            SERVLEVEL,      //サービスレベル
        }

        /// <summary>シフト種別・休日</summary>
        public const string ShiftTypeRest = "REST";

        /// <summary>日付カテゴリ</summary>
        public enum DateCategory : int {
            PublicHoliday = 1, //祝日
            PrivateHoliday,	   //特別休日
            AMOnly,            //AM稼働
            PMOnly,            //PM稼働
        }

        /// <summary>従業員希望ステータス</summary>
        #region EmployeeHopeStatus
        public const string EmployeeHopeStatusOK = "OK";
        public const string EmployeeHopeStatusNO = "NO";
        public const string EmployeeHopeStatusNG = "NG";
        #endregion

        /// <summary>シフトコード・休日</summary>
        public const string RestDayShiftCd = "LAYOFF";

        /// <summary>目的関数の種類</summary>
        #region ObjFuncType
        public const string ObjFuncTypeCost      = "Cost";
        public const string ObjFuncTypeEmpSatis  = "EmpSatis";
        public const string ObjFuncTypeServLevel = "ServLevel";
        #endregion

        /// <summary>目的関数影響度分析軸</summary>
        #region AnalyPivotType
        public const string PivotTypeWorkNum        = "1";
        public const string PivotTypeWorkTime       = "2";
        public const string PivotTypeWorkTimeStress = "3";
        public const string PivotTypeSeveralWorkNum = "4";
        public const string PivotTypeOKWorkNum      = "5";
        public const string PivotTypeNOWorkNum      = "6";
        public const string PivotTypeSubWorkNum     = "7";
        public const string PivotTypeCost           = "8";
        public const string PivotTypeSkillLevel     = "9";
        #endregion

        /// <summary>システムコードカテゴリの目的関数種別</summary>
        public const string SysCatObjFuncType = "ObjFuncType";

        /// <summary>グループと部署のルートコード兼ID</summary>
        public const string RootCodeAndId = "root";

        /// <summary>勤務計画の配置シフトのベース列名</summary>
        public const string ShiftColBaseName = "day{0}_shift_code";

        /// <summary>最適化した結果のステータスコード</summary>
        public enum OptimizeStates : int {
            Success = 0,
            Error,
        }

        /// <summary>従業員制約の条件コード</summary>
        public enum EmpConstrCondition : int {
            GreaterEqual = 0,
            Equal,
            LessEqual,
        }

        /// <summary>シフト選択ダイアログのモード</summary>
        public enum ShiftSelectDialogMode : int {
            ShiftSelectMode = 0,
            ShiftTypeSelectMode,
            AllSelectMode,
        }

        /// <summary>従業員制約要素のカテゴリ</summary>
        public const string EmpConstrElemEmployee  = "EMPLOYEE";
        public const string EmpConstrElemShift     = "SHIFT";
        public const string EmpConstrElemShiftType = "SHIFT_TYPE";

        /// <summary>シフトパターン列の種別</summary>
        public const string ShiftPattarnColShift     = "SHIFT";
        public const string ShiftPattarnColShiftType = "SHIFT_TYPE";

        /// <summary>制約ルールコード</summary>
        public enum ConstrRuleCd : int {
            OneDayOneShift = 1,
            LowestNumber,
            MinMonthlyWorkNum,
            MaxMonthlyWorkNum,
            MaxMonthlyWorkHours,
            MinWeeklyWorkNum,
            MaxWeeklyWorkNum,
            MaxWeeklyWorkHours,
            ConsecutiveWorkDays,
            SuspensionEmployee,
            FixedShift,
            SameShiftPair,
            BanPair,
            WorkNumFilter,
            BannedShift,
            DurationWorkNum,
            FixedShiftType,
            DurationShiftNum,
            DurationWorkHours,
        }

        /// <summary>従業員制約の期間条件</summary>
        public enum ConditionSpan : int {
            Month = 0,
            Week,
        }

        /// <summary>使用できるソルバの種類</summary>
        public enum SolverType {
            Gurobi,
            CBC,
        }

        /// <summary>シフトの休憩種別コード</summary>
        public const string PeriodStatusBreak = "BREAK";
        public const string PeriodStatusPaidBreak = "PAID_BREAK";

        /// <summary>シフトの稼働時のステータスコード</summary>
        public const string PeriodStatusWork = "WORK";

        /// <summary>スケジューリング結果詳細のグラフの種類</summary>
        public enum GraphType : int {
            Line = 1,
            Column,
            Scatter
        }

        /// <summary>スケジューリング結果詳細のグラフの縦軸の種類</summary>
        public enum VerticalAxisType : int {
            HopePassingRate = 1,
            SufficiencyRate,
            OverSufficiencyRate,
            PostedEmployeeRate,
            PostedEmployeeCost,
        }

        /// <summary>スケジューリング結果詳細のグラフの横軸の種類</summary>
        public enum HorizontalAxisType : int {
            Period = 1,
            Day,
            Week,
            DayOfWeek,
            Month,
        }
    }
}
