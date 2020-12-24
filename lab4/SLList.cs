using static Printer;

namespace List
{
  class SLNode
  {
    public int data;
    public SLNode next;
    public SLNode(int data)
    {
      this.data = data;
      next = null;
    }
  }
  class SLList
  {
    public SLNode head;

    public SLList()
    {
      head = null;
    }

    public void AddFirst(int data)
    {
      SLNode node = new SLNode(data);
      if (head == null)
      {
        node.next = null;
        head = node;
        return;
      }
      node.next = head;
      head = node;
    }

    public void Print()
    {
      Printer.BeginColored(Printer.Color.Blue);

      SLNode current = head;
      if (current == null)
      {
        PrintError("Single list is empty.");
        Printer.EndColored();
        return;
      }
      Printer.Print("Single lise: ");
      for (int i = 1; current != null; ++i)
      {
        Printer.Print($"Index: {i}, value: {current.data}");
        current = current.next;
      }
      Printer.EndColored();
    }

  }
}