using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Node
    {
       public string code;
        public int depart, arrival,maxTime;
        public Node left, right;

       public Node(string code,int depart,int arrival)
        {
            this.code = code;
            this.depart = depart;
            this.arrival = arrival;
        }

        public void insertData(ref Node node, string code, int depart, int arrival)
        {
            if (node == null)
            {
                node = new Node( code,  depart,  arrival);

            }

            if (node.depart < depart)
            {
                 insertData(ref node.right,code, depart, arrival);
            }

            if (node.depart > depart)
            {
                 insertData(ref node.left, code, depart, arrival);
            }
            
        }

        public void display(Node n)
        {
            if (n == null)
                return;

            display(n.left);
            Console.Write(" " + n.code);
            display(n.right);
        }
    }
}
