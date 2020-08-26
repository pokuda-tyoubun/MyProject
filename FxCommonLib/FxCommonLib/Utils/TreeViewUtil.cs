using System.Windows.Forms;

namespace FxCommonLib.Utils {
    public class TreeViewUtil {

        /// <summary>
        /// ポジションからノードを取得
        /// </summary>
        /// <param name="Nodes"></param>
        /// <param name="Indexes"></param>
        /// <returns></returns>
        public TreeNode GetNodeFromPosition(TreeNodeCollection Nodes, params int[] Indexes) {
        	int i = 0;
        	TreeNode Node = default(TreeNode);

        	Node = Nodes[Indexes[0]];

        	for (i = 1; i <= Indexes.Length - 1; i++) {
        		Node = Node.Nodes[Indexes[i]];
        	}

        	return Node;
        }

        public static void SelectNodeFromPath(TreeView treeView, string path) {
            TreeNode target = GetTreeNodeFromPath(treeView, treeView.TopNode, path);
            if (target != null) {
                treeView.SelectedNode = target;
            }
        }

        public static TreeNode GetTreeNodeFromPath(TreeView treeView, TreeNode currentNode, string path) {
            TreeNode ret = null;
            foreach (TreeNode child in currentNode.Nodes) {
                if (child.FullPath == path) {
                    return child;
                }
                ret = GetTreeNodeFromPath(treeView, child, path);
                if (ret != null) {
                    return ret;
                }
            }
            return ret;
        }
    }
}
