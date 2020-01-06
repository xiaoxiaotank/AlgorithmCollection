using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// 积分排名
/// 前提：积分为非负整数
/// 参考：https://www.cnblogs.com/hohoa/p/12145689.html 、https://www.cnblogs.com/weidagang2046/archive/2012/03/01/massive-user-ranking.html#!comments
/// 
/// 原理：所构建的树节点均为左节点小于右节点，
///     再将用户积分插入到树之后，每个右节点则存储经过该节点的数量Count
///     当查询Integral排名时，若Integral落在左节点范围内，则之前经过的右节点的Sum(Count)则为大于该Integral的积分数量，也就是排名
///     由于积分数量从0开始，排名从1开始，所以最后要+1
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
