using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class BinarySTree
    {
        Node root,minN;
        int members,temp;
        double minT = 100000;
        List<int> keys = new List<int>();
        LinkedList<string> ErwtimaBC = new LinkedList<string>();

        public BinarySTree() {
            root = null;
            members = 0;
        }

        public void Add(string code, int depart_time, int arrival_time)
        {
            if (root == null)
            {
                Node newnode = new Node(code, depart_time, arrival_time);
                root = newnode;
                keys.Add(depart_time);
                members++;
            }
            else if (!keys.Contains(depart_time)) {
                root.insertData(ref root, code, depart_time, arrival_time);
                keys.Add(depart_time);
                members++;
            }

        }

        public Node SearchNodeAndFindParent(int key, ref Node parent) {
            Node start = root;
            parent = null;
            while (start != null) {

                if (start.depart == key)
                {
                    return start;
                }
                else if (start.depart > key)
                {
                    parent = start;
                    start = start.left;

                }
                else {
                    parent = start;
                    start = start.right;
                }
            }
            return null;
        }

        //Μέθοδος που μας βοηθάει να εντοπίσουμε άν ο κόμβος node έχει παιδιά η όχι.
        //Αν έχει επιστρέφει true αλλίως false.
        public bool hasChildren(Node node) {

            if ((node.left == null) && (node.right == null)) {
                return false;
            }

            return true;
        }

        public Node findNextNode(Node killnode, ref Node parent) {

            Node tmp = killnode.right;
            while (tmp.left != null) {
                parent = tmp;
                tmp = tmp.left;
            }
            //Βρέθηκε ο κόμβος που θα πάρει την θέση του αρχικού κόμβου.
            //Στην συνέχεια διαγράφουμε τον κόμβο που βρέθηκε απο τον αρχικό κόμβο/γονέα.
            parent.left = null;
            return tmp;

        }

        //Μέθοδος η οποία θα καταστρέφει τον κόμβο με το αντίστοιχο key
        public void Kill(int key) {
            //Υπάρχουν 3 περιπτώσεις που μπορεί να βρεθεί ένας κόμβος τον οποίο θέλουμε να
            //καταστρέψουμε.Α) Να μην έχει παιδιά οπότε η καταστροφή του είναι απλή.Β)
            //Να έχει ένα παιδί οπότε στην περίπτωση αυτή αντικαταστούμε τον κόμβο/παιδί
            //με τον αρχικό κόμβο.Γ) Να έχει δύο παιδιά οπότε θα πρέπει να βρούμε ποιός
            //κόμβος/παιδί θα πρέπει να αντικαταστήση τον κόμβο/γονέα.

            //Αρχικά θα βρούμε άν υπάρχει ο κόμβος τον οποίο θέλουμε να διαγράψουμε αλλα και
            //τον γονέα του έτσι ώστε να συνδέσουμε τον γονέα με το παιδί
            Node parent = null;

            //Όταν τελειώσει η επόμενη μέθοδος θα έχουμε τον κόμβο που πρέπει να τερματίσουμε
            //αλλά και τον γονέα του.
            Node killNode = SearchNodeAndFindParent(key, ref parent);

            //Στην συνέχεια θα πάρουμε περιπτώσεις για να βρούμε σε ποιά περίπτωση βρίσκεται
            //ο κόμβος.

            //Περίπτωση Α) οπότε ο κόμβος είναι φύλο οπότε ο δείκτης του κόμβου/γονέα γίνεται
            //null. 
            #region CaseA
            if (!hasChildren(killNode)) {
                //Περίπτωση που ο κόμβος που θέλουμε να καταστρέψουμε είναι η ρίζα
                if (parent == null) {
                    root = null;
                    return;
                }

                //Άν ο αριστερός δείκτη του γονέα ισούται με τον κόμβο τότε ο δείκτης γίνεται
                //null και αντίστοιχα άν είναι ο δεξιός δείκτης.
                if (parent.left == killNode)
                    parent.left = null;
                else
                    parent.right = null;
                members--;

                return;
            }
            #endregion

            //Περίπτωση B) ο κόμβος έχει ένα παιδί, οπότε βρίσκουμε ποιό παιδί είναι
            //(αριστερό η δεξί) και βάζουμε τον δείκτη του γονέα του κόμβου που θέλουμε 
            //να δείχνει στο παιδί. parent->killNode->child  parent->child.
            #region CaseB
            //Άν δεν έχει αριστερό παιδί τότε δείχνουμε στο δεξί
            if (killNode.left == null) {

                //Σε περίπτωση που βρισκόμαστε στην ρίζα.
                if (parent == null) {
                    root = killNode.right;
                    return;
                }

                //Εξετάζουμε άν ο κόμβος που θέλουμε να καταστρέψουμε είναι αριστερό παιδί
                //η δεξί. Αν είναι αριστερό τότε την θέση του θα πάρει το παιδί του και 
                //αντίστοιχα άν είναι δεξί.
                if (parent.left == killNode)
                    parent.right = killNode.right;
                else
                    parent.left = killNode.right;
                killNode = null; // Clean up the deleted node
                members--;
                return;
            }
            // One of the children is null, in this case
            // delete the node and move child up
            if (killNode.right == null)
            {
                // Special case if we're at the root			
                if (parent == null) {
                    root = killNode.left;
                    return;
                }

                // Identify the child and point the parent at the child
                if (parent.left == killNode)
                    parent.left = killNode.left;
                else
                    parent.right = killNode.left;
                killNode = null; // Clean up the deleted node
                members--;
                return;
            }

            #endregion

            //Περίπτωση C) ο κόμβος έχει δύο παιδιά, οπότε πρέπει να επιλέξουμε ποιό παιδί
            //είναι κατάλληλο για να αντικαταστήση τον κόμβο που θα καταστραφεί.Υπάρχουν
            //δύο περιπτώσεις, ή να βρούμε τον κόμβο με την αμέσως προηγούμενη τιμή ή
            //τον κόμβο με την επόμενη τιμή. Για την πρώτη περίπτωση ο κόμβος με την 
            //αμέσως προηγούμενη τιμή βρίσκεται στο πιό δεξιά κόμβο στο αριστερό υποδέντρο,
            //και στην δεύτερη στον πιο αριστερό κόμβο στο δεξί υποδέντρο. Στην συγκεκριμένη
            //περίπτωση θα βρούμε την αμέσως επόμενη τιμή. 
            #region CaseC

            Node helpNode = null;

            //Ο κόμβος successor κρατάει τον κόμβο που θα αντικαταστήσει τον κόμβο που
            //θέλουμε να διαγράψουμε.
            Node successor = findNextNode(killNode, ref helpNode);

            killNode.code = successor.code;
            killNode.arrival = successor.arrival;
            killNode.depart = successor.depart;
            killNode.maxTime = successor.maxTime;

            #endregion
        }

        //Μέθοδος για να βρούμε ποιές πτήσεις είναι σε εξέλιξη. 
        public void TraverseListAndAdd(int limit1, int limit2)
        {
            Node start = root;

            TraverseList(root, limit1, limit2);

        }

        //Μέθοδος στην οποία διασχίζουμε ολο το δυαδικό δέντρο και συγκρίνουμε τις ώρες άφιξης και 
        //αναχώρησης να βρούμε ποίες πτήσεις είναι σε εξέλιξη, άν βρεθεί πτήση που ικανοποιεί τις 
        //προυποθέσεις προστείθετε στην λίστα ErwtimaBC. Για να διασχίσουμε τους κόμβους
        //χρησιμοποιούμε αναδρομικότητα.
        public void TraverseList(Node root, int limit1, int limit2) {

            if ((root.arrival <= limit1 || root.depart<=limit2 ) ||
                 (root.depart<=limit1 || root.depart<=limit2)) {
                ErwtimaBC.AddFirst(root.code);
            }

            if (root.left != null)
            {
                TraverseList(root.left,limit1,limit2);
            }
            
            if (root.right != null)
            {
                TraverseList(root.right,limit1,limit2);
            }
        }

        //Μέθοδος για να βρούμε την πτήση με την ελάχιστη απόσταση απο την ώρα h
        public void FindMin(int h){
             Node start = root;
            TraverseList(start,h);
            Console.WriteLine(minN.code);
        }

        //Μέθοδος που διασχίζει το δυαδικό δέντρο, υπολογίζει την ελάχιστη διαφορά μεταξύ των πτήσεων και
        //αποθηκεύει την ελάχιστη τιμή και αντίστοιχο κωδικό πτήσης στις μεταβλητές minN και minT.
        public void TraverseList(Node start,int h) {
            temp = Math.Abs(start.depart - h);
            if (temp < minT) {
                minN = start;
                minT = temp;
            }

            if (start.left != null)
            {
                TraverseList(start.left, h);
            }

            if (start.right != null)
            {
                TraverseList(start.right,h);
            }
        }

        public void display()
        {
            if (!(root==null))
                root.display(root);
        }
        public void displayE() {
            foreach (string s in ErwtimaBC)
            {
                Console.WriteLine(s+" ");
            }
        }
        public void showroot() {
            Console.Write(" " + root.code);
        }

    }
}
