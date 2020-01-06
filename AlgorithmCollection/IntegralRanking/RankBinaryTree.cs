using System;
using System.Collections.Generic;
using System.Text;

namespace IntegralRanking
{
    /// <summary>
    /// 排名二叉树
    /// </summary>
    public class RankBinaryTree
    {
        private readonly TreeNode _root;

        public RankBinaryTree(int minValue, int maxValue)
        {
            _root = new TreeNode()
            {
                MinValue = minValue,
                MaxValue = maxValue + 1,
                Height = 1,
            };
            _root.Left = CreateChildNode(_root, true);
            _root.Right = CreateChildNode(_root, false);
        }

        /// <summary>
        /// 插入值/积分
        /// </summary>
        public void InsertValue(int value)
        {
            InternalInsertValue(_root, value);
        }

        /// <summary>
        /// 根据值/积分获取排名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int GetRank(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "不能为负数");

            return InternalGetRank(_root, value);
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="isLeft">
        ///     <c>true</c>：左节点    
        ///     <c>false</c>：右节点
        /// </param>
        /// <returns></returns>
        private static TreeNode CreateChildNode(TreeNode parent, bool isLeft)
        {
            if (parent == null) return null;

            var halfValue = (parent.MinValue + parent.MaxValue) / 2;
            var node = new TreeNode()
            {
                MinValue = isLeft ? parent.MinValue : halfValue,
                MaxValue = isLeft ? halfValue : parent.MaxValue,
                Height = parent.Height + 1,
                Parent = parent
            };
            if (node.MinValue < node.MaxValue - 1)
            {
                node.Left = CreateChildNode(node, true);
                node.Right = CreateChildNode(node, false);
            }

            return node;
        }

        private void InternalInsertValue(TreeNode node, int value)
        {
            if (node == null) return;

            if (InRange(node, value))
            {
                node.Count++;
                InternalInsertValue(node.Left, value);
                InternalInsertValue(node.Right, value);
            }
        }

        private int InternalGetRank(TreeNode node, int value)
        {
            if (node.Left == null || node.Right == null) return 1;

            if (InRange(node.Left, value))
            {
                return node.Right.Count + InternalGetRank(node.Left, value);
            }

            return InternalGetRank(node.Right, value);
        }

        private static bool InRange(TreeNode node, int value)
        {
            return value >= node.MinValue && value < node.MaxValue;
        }
    }

    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// 落在该节点的积分个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 高度/层数 （从1开始）
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public TreeNode Parent { get; set; }

        /// <summary>
        /// 左子
        /// </summary>
        public TreeNode Left { get; set; }

        /// <summary>
        /// 右子
        /// </summary>
        public TreeNode Right { get; set; }
    }
}
