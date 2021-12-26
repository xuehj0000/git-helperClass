using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic.Devices;

namespace HD.Common.Cache
{
    /// <summary>
    /// 当前计算机 cpu使用率，已使用内存，总内存值
    /// 发布到iis上时获取不到值，需要配置一下iis:https://www.cnblogs.com/linkbiz/p/5190485.html
    /// </summary>
    public class CpuAndMemory
    {

        readonly PerformanceCounter cpu;
        readonly ComputerInfo cinf;

        public CpuAndMemory()
        {
            this.cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            this.cinf = new ComputerInfo();
        }

        /// <summary>
        /// 获取 cpu 使用率
        /// </summary>
        public double GetCpuPercent()
        {
            var percentage = this.cpu.NextValue();
            Thread.Sleep(1000);
            percentage = this.cpu.NextValue();
            return Math.Round(percentage, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        ///  获取总内存,M
        /// </summary>
        public double GetTotalMemory()
        {
            return Math.Round(((double)(this.cinf.TotalPhysicalMemory) / Convert.ToDouble(1024) / Convert.ToDouble(1024)), 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 已使用内存
        /// </summary>
        public double HasUsedMemory()
        {
            return Math.Round(((double)(this.cinf.TotalPhysicalMemory - this.cinf.AvailablePhysicalMemory) / Convert.ToDouble(1024) / Convert.ToDouble(1024)), 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 内存使用率
        /// </summary>
        /// <returns></returns>
        public double GetMemoryPercent()
        {
            var usedMem = this.cinf.TotalPhysicalMemory - this.cinf.AvailablePhysicalMemory;//总内存减去可用内存
            return Math.Round((double)(usedMem / Convert.ToDecimal(this.cinf.TotalPhysicalMemory) * 100), 2, MidpointRounding.AwayFromZero);
        }

        public HardDiskInfo GetHardDiskInfoByName(string diskName)
        {
            DriveInfo drive = new DriveInfo(diskName);
            return new HardDiskInfo { FreeSpace = GetDriveData(drive.AvailableFreeSpace), TotalSpace = GetDriveData(drive.TotalSize), Name = drive.Name };
        }

        public IEnumerable<HardDiskInfo> GetAllHardDiskInfo()
        {
            List<HardDiskInfo> infos = new List<HardDiskInfo>();
            foreach (var d in DriveInfo.GetDrives())
            {
                if (d.IsReady)
                {
                    HardDiskInfo h = new HardDiskInfo
                    {
                        Name = d.Name,
                        FreeSpace = GetDriveData(d.AvailableFreeSpace),
                        TotalSpace = GetDriveData(d.TotalSize)
                    };
                    infos.Add(h);
                }
            }
            return infos;
        }

        /// <summary>
        /// 通过方法，
        /// </summary>
        private string GetDriveData(long data)
        {
            return (data / Convert.ToDouble(1024) / Convert.ToDouble(1024) / Convert.ToDouble(1024)).ToString("0.00");
        }

        public void Dispose()
        {
            this.cpu.Dispose();
        }
    }

    #region 扩展类
    /// <summary>
    /// 扩展类
    /// </summary>
    public class HardDiskInfo
    {
        public string Name { get; set; }
        public string FreeSpace { get; set; }
        public string TotalSpace { get; set; }
    }

    #endregion

}
