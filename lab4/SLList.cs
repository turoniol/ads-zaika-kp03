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

    private SLNode Tail()
    {
      SLNode current = head;
      if (current == null) return null;
      while (current.next != null)
      {
        current = current.next;
      }
      return current;
    }

    public void AddLast(int data)
    {
      SLNode node = new SLNode(data);
      if (head == null)
      {
        node.next = null;
        head = node;
        return;
      }
      Tail().next = node;
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