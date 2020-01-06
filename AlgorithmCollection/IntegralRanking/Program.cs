using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// 积分排名
/// 前提：积分为非负整数
/// 参考：https://www.cnblogs.com/hohoa/p/12145689.html 、https://www.cnblogs.com/weidagang2046/archive/2012/03/01/massive-user-ranking.html#!comments
/// </summary>
namespace IntegralRanking
{
    class Program
    {
        private const int Min_Integral = 0;
        private const int Max_Integral = 1_000_000;

        static void Main(string[] args)
        {
            var integrals = CreateSampleIntegrals();

            var sw = Stopwatch.StartNew();
            var rankTree = new RankBinaryTree(Min_Integral, Max_Integral);
            sw.Stop();
            Console.WriteLine($"创建树：{sw.ElapsedMilliseconds} ms");

            sw.Restart();
            foreach (var integral in integrals)
            {
                rankTree.InsertValue(integral);
            }
            sw.Stop();
            Console.WriteLine($"插入数据：{sw.ElapsedMilliseconds} ms");

            var myIntegrals = new[] { 100, 1_000_000 };
            foreach (var intergral in myIntegrals)
            {
                sw.Restart();
                var rank = rankTree.GetRank(intergral);
                sw.Stop();
                Console.WriteLine($"{intergral}：{rank} {sw.ElapsedMilliseconds} ms");
            }

            Console.ReadKey();
        }

        static IEnumerable<int> CreateSampleIntegrals()
        {
            return Enumerable.Range(Min_Integral, Max_Integral - Min_Integral + 1);
        }
    }
}
