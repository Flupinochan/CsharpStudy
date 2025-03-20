using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace FrameResizer.Utils;
public class CustomiseImage
{
    /// <summary>
    /// 画像をリサイズ、枠線を描画して保存する
    /// </summary>
    /// <param name="sourceImagePath">元の画像ファイルのパス</param>
    /// <param name="outputImagePath">出力する画像ファイルのパス</param>
    /// <param name="outputHeight">リサイズする縦幅 ※0の場合は横幅を基準</param>
    /// <param name="outputWidth">リサイズする横幅 ※0の場合は縦幅を基準</param>
    /// <param name="borderSize">枠線の太さ</param>
    /// <param name="borderColor">枠線の色</param>
    public static void Convert(String sourceImagePath, String outputImagePath,
                                          Int32 outputHeight, Int32 outputWidth,
                                          Int32 borderSize, Color borderColor)
    {
        using Image tmpImage = Image.Load(sourceImagePath);

        // アスペクト比を維持したリサイズ処理
        /// 元の画像の比率を取得
        Double aspectRatio = (Double)tmpImage.Width / tmpImage.Height;
        /// Height or Width を基準にリサイズ ※どちらかは0にする
        if(outputWidth > 0 && outputHeight == 0)
            outputHeight = (Int32)(outputWidth / aspectRatio);
        if(outputHeight > 0 && outputWidth == 0)
            outputWidth = (Int32)(outputHeight * aspectRatio);
        /// borderの長さだけ余分に縮小
        Int32 innerWidth = outputWidth - (borderSize * 2);
        Int32 innerHeight = outputHeight - (borderSize * 2);
        /// リサイズ実行
        tmpImage.Mutate(ctx => ctx.Resize(innerWidth, innerHeight));

        // 出力サイズの画像を作成し、背景(border色)で塗り、縮小された画像を描画
        /// border色の背景色の画像を作成
        using Image<Rgba32> outputImage = new Image<Rgba32>(outputWidth, outputHeight);
        outputImage.Mutate(ctx => ctx.Fill(borderColor));
        /// Pointで左上を基準にborderのサイズだけずらして描画
        outputImage.Mutate(ctx => ctx.DrawImage(tmpImage, new Point(borderSize, borderSize), 1f));

        // 拡張子(jpg, png)に応じて保存
        /// 拡張子を取得
        String extension = System.IO.Path.GetExtension(outputImagePath);
        /// 保存実行
        if(extension == ".png")
            outputImage.Save(outputImagePath, new PngEncoder());
        if(extension == ".jpg" || extension == ".jpeg")
            outputImage.Save(outputImagePath, new JpegEncoder() { Quality = 100 });
    }


    /// <summary>
    /// リサイズのみ実行
    /// </summary>
    /// <param name="sourceImagePath">元の画像ファイルのパス</param>
    /// <param name="outputImagePath">出力する画像ファイルのパス</param>
    /// <param name="outputHeight">リサイズする縦幅 ※0の場合は横幅を基準</param>
    /// <param name="outputWidth">リサイズする横幅 ※0の場合は縦幅を基準</param>
    public static void Convert(String sourceImagePath, String outputImagePath,
                                          Int32 outputHeight, Int32 outputWidth)
    {
        using Image outputImage = Image.Load(sourceImagePath);

        // アスペクト比を維持したリサイズ処理
        /// 元の画像の比率を取得
        Double aspectRatio = (Double)outputImage.Width / outputImage.Height;
        /// Height or Width を基準にリサイズ ※どちらかは0にする
        if(outputWidth > 0 && outputHeight == 0)
            outputHeight = (Int32)(outputWidth / aspectRatio);
        if(outputHeight > 0 && outputWidth == 0)
            outputWidth = (Int32)(outputHeight * aspectRatio);
        /// リサイズ実行
        outputImage.Mutate(ctx => ctx.Resize(outputWidth, outputHeight));

        // 拡張子(jpg, png)に応じて保存
        /// 拡張子を取得
        String extension = System.IO.Path.GetExtension(outputImagePath);
        /// 保存実行
        if(extension == ".png")
            outputImage.Save(outputImagePath, new PngEncoder());
        if(extension == ".jpg" || extension == ".jpeg")
            outputImage.Save(outputImagePath, new JpegEncoder() { Quality = 100 });
    }
}
