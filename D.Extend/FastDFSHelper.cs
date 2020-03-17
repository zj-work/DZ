using FastDFS.Client;
using FastDFS.Client.Common;
using FastDFS.Client.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PerkerAI.SIS.Browser
{
    /// <summary>
    /// 负责人: QFN
    /// 时  间: 2018-10-15 15:28:37
    /// 功  能: FastDFS 文件上传下载
    /// 描  叙: 
    /// app.config配置如下所示
    /// <appSettings>
    ///  <add key = "fastdfs_trackers" value="192.168.10.128" />
    /// <add key = "fastdfs_storages" value="192.168.10.128" />
    ///  <add key = "fastdfs_port" value="22122" />
    ///  <add key = "fastdfs_groupname" value="group1" />
    ///  <add key = "fastdfs_maxsize" value="10000000" />
    ///  <add key = "fastdfs_type" value="gif,jpg,jpeg,png,bmp,zip,rar,layout" />
    ///  <add key = "fastdfs_type_file" value="txt,xlsx,zip,rar" />
    ///</appSettings>
    /// 
    /// </summary>

    public class FastDFSHelper
    {
        private List<IPEndPoint> trackerIPs = new List<IPEndPoint>();
        private IPEndPoint endPoint;
        private StorageNode storageNode;
        private string groupName = ConfigurationManager.AppSettings["fastdfs_groupname"];
        /// <summary>
        /// 链接 FASTDFS
        /// </summary>
        public FastDFSHelper()
        {
            string[] trackers = ConfigurationManager.AppSettings["fastdfs_trackers"].Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
            string[] storages = ConfigurationManager.AppSettings["fastdfs_storages"].Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
            int port = int.Parse(ConfigurationManager.AppSettings["fastdfs_port"]);

            foreach (var onetracker in trackers)
            {
                trackerIPs.Add(new IPEndPoint(IPAddress.Parse(onetracker), port));
            }
            ConnectionManager.Initialize(trackerIPs);
            storageNode = FastDFSClient.GetStorageNode(groupName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="prefixName">从文件前缀名称</param>
        /// <param name="fileExt">文件后缀名</param>
        /// <param name="slaveFileName">返回从文件路径</param>
        /// <returns>返回主文件路径</returns>
        public string UploadFile(Stream fileStream, string prefixName, string fileExt, out string slaveFileName)
        {

            byte[] content = new byte[fileStream.Length];

            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                content = reader.ReadBytes((int)fileStream.Length);
            }

            //主文件
            string fileName = FastDFSClient.UploadFile(storageNode, content, fileExt);
            var info = FastDFSClient.GetFileInfo(storageNode, fileName);
            //从文件
            slaveFileName = FastDFSClient.UploadSlaveFile(groupName, content, fileName, prefixName, fileExt);
            var slaveInfo = FastDFSClient.GetFileInfo(storageNode, slaveFileName);
            return fileName;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name=""></param>
        /// <returns>文件流</returns>
        public FileStream DownloadFile(string fileName)
        {

            FDFSFileInfo fileInfo = FastDFSClient.GetFileInfo(storageNode, fileName);

            FileStream fileStream = new FileStream(Path.GetTempFileName(), FileMode.Create);
            if (fileInfo.FileSize >= 1024)//如果文件大小大于1KB  分次写入
            {
                long offset = 0;
                long len = 1024;
                while (len > 0)
                {
                    byte[] buffer = new byte[len];
                    buffer = FastDFSClient.DownloadFile(storageNode, fileName, offset, len);
                    fileStream.Write(buffer, 0, int.Parse(len.ToString()));
                    fileStream.Flush();
                    offset = offset + len;
                    len = (fileInfo.FileSize - offset) >= 1024 ? 1024 : (fileInfo.FileSize - offset);
                }
                return fileStream;
            }
            else//如果文件大小小小于1KB  直接写入文件
            {
                byte[] buffer = new byte[fileInfo.FileSize];
                buffer = FastDFSClient.DownloadFile(storageNode, fileName);
                //FileStream fileStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Flush();
                fileStream.Close();
                return fileStream;
            }
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name=""></param>
        /// <returns>文件流</returns>
        public byte[] DownloadFileByte(string fileName)
        {
            FDFSFileInfo fileInfo = FastDFSClient.GetFileInfo(storageNode, fileName);
            byte[] buffer = new byte[fileInfo.FileSize];
            buffer = FastDFSClient.DownloadFile(storageNode, fileName);

            FileStream fs = new FileStream("D://down123.png", FileMode.Create, FileAccess.Write);
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
            fs.Close();

            return buffer;
        }

        /// <summary>
        /// 下载文件 流模式
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name=""></param>
        /// <returns>文件流</returns>
        public Stream DownloadFileStream(string fileName)
        {
            FDFSFileInfo fileInfo = FastDFSClient.GetFileInfo(storageNode, fileName);

            Stream stream = new MemoryStream();


            if (fileInfo.FileSize >= 1024)//如果文件大小大于1KB  分次写入
            {
                long offset = 0;
                long len = 1024;
                while (len > 0)
                {
                    byte[] buffer = new byte[len];
                    buffer = FastDFSClient.DownloadFile(storageNode, fileName, offset, len);
                    stream.Write(buffer, 0, int.Parse(len.ToString()));
                    stream.Flush();
                    offset = offset + len;
                    len = (fileInfo.FileSize - offset) >= 1024 ? 1024 : (fileInfo.FileSize - offset);
                }

            }
            else//如果文件大小小小于1KB  直接写入文件
            {
                byte[] buffer = new byte[fileInfo.FileSize];
                buffer = FastDFSClient.DownloadFile(storageNode, fileName);
                //FileStream fileStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// 根据文件名称获取文件浏览路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFilePath(string fileName)
        {
            string trackers = ConfigurationManager.AppSettings["fastdfs_storages"];
            string groupname = ConfigurationManager.AppSettings["fastdfs_groupname"];
            return trackers + "/" + groupname + "/" + fileName;
        }
    }
}
