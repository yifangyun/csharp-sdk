namespace Yfy.Api.Files
{
    using Newtonsoft.Json;
    using Yfy.Api.Items;

    /// <summary>
    /// 通用文件对象
    /// </summary>
    public class YfyFile : YfyItem
    {
        /// <summary>
        /// 文件sha1值
        /// </summary>
        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        /// <summary>
        /// 文件的评论数
        /// </summary>
        [JsonProperty("comments_count")]
        public long CommentsCount { get; set; }

        /// <summary>
        /// 文件的扩展名
        /// </summary>
        [JsonProperty("extension_category")]
        public string ExtensionCategory { get; set; }
    }

    /// <summary>
    /// 预览文件类型
    /// </summary>
    public enum PreviewType
    {
        /// <summary>
        /// 表示像素最高为64 * 64的图片，常用于缩略图，
        /// 支持的文件格式，psd,png,jpg,jpeg,jpf,jp2,gif,tif,tiff,bmp,aix,ico,svg,ps,eps,ai
        /// </summary>
        thumbnail,

        /// <summary>
        /// 表示像素最高为128 * 128的图片，常用于缩略图，
        /// 支持的文件格式，psd,png,jpg,jpeg,jpf,jp2,gif,tif,tiff,bmp,aix,ico,svg,ps,eps,ai
        /// </summary>
        low_quality,

        /// <summary>
        /// 表示像素最高为1024 * 1024的图片，标准预览格式，
        /// 支持的文件格式，doc,docx,odt,rtf,wps,yxls,xls,xlsx,ods,csv,et,ppt,pptx,odp,dps,markdown,md,mdown,html,xhtml,htm,tsv,as,as3,asm,
        /// bat,c,cc,cmake,cpp,cs,csh,css,cxx,diff,erb,groovy,h,haml,hh,java,js,less,m,make,ml,mm,php,pl,plist,properties,py,rb,sass,scala,
        /// scm,script,sh,sml,sql,txt,vi,vim,xml,xsd,xsl,xslt,yaml,pdf,psd,png,jpg,jpeg,jpf,jp2,gif,tif,tiff,bmp,aix,ico,svg,ps,eps,ai,dwg,dxf
        /// </summary>
        medium_quality,

        /// <summary>
        /// 表示像素最高为2048 * 2048的图片，常用于高清图，
        /// 支持的文件格式，psd,png,jpg,jpeg,jpf,jp2,gif,tif,tiff,bmp,aix,ico,svg,ps,eps,ai,dwg,dxf
        /// </summary>
        high_quality,

        /// <summary>
        /// pdf文件
        /// 支持的文件格式，doc,docx,odt,rtf,wps,yxls,xls,xlsx,ods,csv,et,ppt,pptx,odp,dps,markdown,md,mdown,html,xhtml,htm,tsv,as,as3,asm,bat,c,
        /// cc,cmake,cpp,cs,csh,css,cxx,diff,erb,groovy,h,haml,hh,java,js,less,m,make,ml,mm,php,pl,plist,properties,py,rb,sass,scala,scm,script,
        /// sh,sml,sql,txt,vi,vim,xml,xsd,xsl,xslt,yaml,pdf,dwg,dxf
        /// </summary>
        pdf,
    }
}
