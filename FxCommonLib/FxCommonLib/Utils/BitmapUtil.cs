using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace FxCommonLib.Utils {


    /// <summary>
    /// ビットマップ画像操作ユーティリティ
    /// </summary>
    //HACK 機能が増えたら部品登録
    public class BitmapUtil {

        #region Constants

        /// <summary>画像品質:変換なし</summary>
        public readonly int no_change_image_quality = -1;

        /// <summary>品質変換サポートする画像フォーマット</summary>
        private readonly List<ImageFormat> changeImageQualitysupportFormats =
            new List<ImageFormat>() {
                ImageFormat.Bmp,
                //ImageFormat.Emf,
                ImageFormat.Exif,
                //ImageFormat.Gif, //NOTE:Gifの品質を変換しサイズを落とすとアニメーション設定が失われる
                //ImageFormat.Icon,
                ImageFormat.Jpeg,
                ImageFormat.MemoryBmp,
                //ImageFormat.Png, //NOTE:PNGの品質を変換しサイズを落とすと透過設定が失われる
                ImageFormat.Tiff,
                //ImageFormat.Wmf
            };

        #endregion Constants
        #region PublicMethods


        public Bitmap GetBitmapFromURL(string url) { 
            int buffSize = 65536; // 一度に読み込むサイズ
            MemoryStream imgStream = new MemoryStream();
         
            if (url == null || url.Trim().Length <= 0) {
                return null;
            }
         
            //----------------------------
            // Webサーバに要求を投げる
            //----------------------------
            WebRequest req = WebRequest.Create(url);
            BinaryReader reader = new BinaryReader(req.GetResponse().GetResponseStream());
         
            //--------------------------------------------------------
            // Webサーバからの応答データを取得し、imgStreamに保存する
            //--------------------------------------------------------
            while (true) {
                byte[] buff = new byte[buffSize];
         
                // 応答データの取得
                int readBytes = reader.Read(buff, 0, buffSize);
                if (readBytes <= 0 ) {
                    // 最後まで取得した->ループを抜ける
                    break;
                }
         
                // バッファに追加
                imgStream.Write( buff, 0, readBytes );
            }
         
            return new Bitmap(imgStream);
        }

        /// <summary>
        /// サイズ変更
        /// </summary>
        /// <param name="src">元のビットマップ画像</param>
        /// <param name="w">変更後の幅</param>
        /// <param name="h">変更後の高さ</param>
        /// <returns></returns>
        public Bitmap Resize(Bitmap src, int w, int h) {
            Bitmap dest = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(dest);

            foreach (InterpolationMode im in Enum.GetValues(typeof(InterpolationMode))) {
                if (im == InterpolationMode.Invalid) {
                    continue;
                }
                g.InterpolationMode = im;
                g.DrawImage(src, 0, 0, w, h);
            }

            return dest;
        }

        /// <summary>
        /// BitmapオブジェクトをJpegファイルとして保存
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="savePath"></param>
        /// <param name="quality">画像品質(Encoder.Quality フィールド)（0-100）</param>
        public void SaveAsJpeg(Bitmap bmp, string savePath, int quality) {

            EncoderParameters eps = new EncoderParameters(1);
            //品質を指定
            EncoderParameter ep = new EncoderParameter(Encoder.Quality, (long)quality);
            //EncoderParametersにセットする
            eps.Param[0] = ep;
            ImageCodecInfo ici = GetEncoderInfo(ImageFormat.Jpeg);
            //保存する
            bmp.Save(savePath, ici, eps);
        }
        /// <summary>
        /// ファイルを指定されたパスへ登録 ファイルが画像である場合は指定した品質レベルに変換
        /// </summary>
        /// <param name="srcFilePath">コピー元ファイルパス</param>
        /// <param name="destFilePath">コピー先ファイルパス</param>
        /// <param name="quality">画像品質(Encoder.Quality フィールド)（0-100）</param>
        /// <param name="doMove">true:元ファイル削除</param>
        /// <exception cref="System.Exception"></exception>
        public void SaveFileWithImageQuality(string srcFilePath, string destFilePath, int quality, bool doMove) {

            System.Drawing.Bitmap bmp = null;
            System.Drawing.Imaging.EncoderParameters eps = null;

            bool doQualityChange = false;

            if (quality != this.no_change_image_quality) {
                doQualityChange = IsChangeQualityImageFile(srcFilePath);
            }

            try {

                if (doQualityChange) {
                    // 画像品質を変更する

                    //画像ファイルを読み込む
                    bmp = new System.Drawing.Bitmap(srcFilePath);

                    //EncoderParameterオブジェクトを1つ格納できる
                    //EncoderParametersクラスの新しいインスタンスを初期化
                    //ここでは品質のみ指定するため1つだけ用意する
                    eps = new System.Drawing.Imaging.EncoderParameters(1);
                    //品質を指定
                    System.Drawing.Imaging.EncoderParameter ep =
                        new System.Drawing.Imaging.EncoderParameter(
                        System.Drawing.Imaging.Encoder.Quality, (long)quality);
                    //EncoderParametersにセットする
                    eps.Param[0] = ep;

                    //イメージエンコーダに関する情報を取得する
                    //System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo(imageFormat);
                    System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo(ImageFormat.Jpeg); // Jpegエンコーダでないとサイズが圧縮できない


                    //保存する
                    bmp.Save(destFilePath, ici, eps);

                    // メモリ開放
                    bmp.Dispose();
                    //eps.Dispose();

                    if (doMove) {
                        // 元ファイルの削除
                        FileUtil.DeleteFile(srcFilePath);
                    }

                } else {
                    // 品質変換を行わない

                    if (doMove) {
                        File.Move(srcFilePath, destFilePath);
                    } else {
                        File.Copy(srcFilePath, destFilePath);
                    }
                }

            } catch (Exception e) {
                throw e;

            } finally {

                if (bmp != null) {
                    bmp.Dispose();
                }

                if (eps != null) {
                    eps.Dispose();
                }
            }
        }

        /// <summary>
        /// 指定されたファイルパスより画像ファイルか否かを返却
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns></returns>
        public bool IsValidImage(string filePath) {
            return (this.GetImageFormatFromFile(filePath) != null);
        }

        /// <summary>
        /// 指定されたファイルパスが画像であればImageFormatを返却
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ImageFormat</returns>
        public ImageFormat GetImageFormatFromFile(string filePath) {

            try {
                if (!File.Exists(filePath)) { return null; }

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (Image targetImage = Image.FromStream(fileStream)) {
                    return targetImage.RawFormat;
                }
            } catch (Exception) {
                return null;
            }
        }
        #endregion PublicMethods
        #region PrivateMethods
        /// <summary>
        /// ファイルが品質変換サポートとなる対象か
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool IsChangeQualityImageFile(string filePath) {

            // イメージフォーマットを取得
            ImageFormat imageFormat = null;
            imageFormat = GetImageFormatFromFile(filePath);

            // 品質変換サポートする画像か
            return (this.changeImageQualitysupportFormats.Contains(imageFormat));

        }
        /// <summary>
        /// MimeTypeで指定されたImageCodecInfoを探して返却
        /// </summary>
        /// <param name="mimeType">ex."image/jpeg"</param>
        /// <returns></returns>
        private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType) {
            //GDI+ に組み込まれたイメージ エンコーダに関する情報をすべて取得
            System.Drawing.Imaging.ImageCodecInfo[] encs =
                System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            //指定されたMimeTypeを探して見つかれば返す
            foreach (System.Drawing.Imaging.ImageCodecInfo enc in encs) {
                if (enc.MimeType == mimeType) {
                    return enc;
                }
            }
            return null;
        }
        /// <summary>
        /// ImageFormatで指定されたImageCodecInfoを探して返却
        /// </summary>
        /// <param name="f">ImageFormat</param>
        /// <returns></returns>
        private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(System.Drawing.Imaging.ImageFormat f) {
            System.Drawing.Imaging.ImageCodecInfo[] encs =
                System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            foreach (System.Drawing.Imaging.ImageCodecInfo enc in encs) {
                if (enc.FormatID == f.Guid) {
                    return enc;
                }
            }
            return null;
        }
        #endregion PrivateMethods
    }
}
