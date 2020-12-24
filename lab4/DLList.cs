using static Printer;

namespace List
{
  class DLNode
  {
    public int data;
    public DLNode prev;
    public DLNode next;
    public DLNode(int data)
    {
      prev = null;
      next = null;
      this.data = data;
    }
  }
  class DLList
  {
    public int size;

    public DLNode head;

    public DLNode tail;

    public DLList(int data)
    {
      head = new DLNode(data);
      AddFirstElement(head);
    }

    public DLList()
    {
      head = null;
      tail = null;
      size = 0;
    }

    private void AddToSize() => size += 1;

    private void RemoveToSize() => size -= 1;

    private bool IsHeadNull()
    {
      bool res = (head == null);
      if (res)
      {
        Printer.PrintError("List is empty.");
        return true;
      }
      return false;
    }

    public void Print()
    {
      Printer.Color color = Printer.Color.Yellow;
      DLNode node = head;
      Printer.PrintColor("List of DLL elements: ", color);
      if (IsHeadNull())
      {
        return;
      }

      for (int i = 1; i < size; ++i)
      {
        Printer.PrintColor($"Index: {i}, data: {node.data}", color);
        node = node.next;
      }
      Printer.PrintColor($"Index: {size}, data: {node.data}", color);
    }

    private void AddPrev(DLNode forAdding, DLNode current)
    {
      forAdding.next = current;
      forAdding.prev = current.prev;
      current.prev.next = forAdding;
      current.prev = forAdding;
      AddToSize();
    }

    private void AddNext(DLNode forAdding, DLNode current)
    {
      forAdding.prev = current;
      forAdding.next = current.next;
      current.next.prev = forAdding;
      current.next = forAdding;
      AddToSize();
    }

    private void AddFirstElement(DLNode node)
    {
      head = node;
      tail = head;
      head.next = tail;
      head.prev = tail;
      tail.prev = head;
      tail.next = head;
      size = 1;
    }

    private void AddPrevElement(DLNode forAdding, DLNode current)
    {
      if (current != null)
      {
        AddPrev(forAdding, current);
      }
      else
      {
        AddFirstElement(forAdding);
      }
    }

    private void AddNextElement(DLNode forAdding, DLNode current)
    {
      if (current != null)
      {
        AddNext(forAdding, current);
      }
      else
      {
        AddFirstElement(forAdding);
      }
    }

    public void AddFirst(int data)
    {
      DLNode node = new DLNode(data);
      AddPrevElement(node, head);
      head = node;
      Print();
    }

    public void AddLast(int data)
    {
      DLNode node = new DLNode(data);
      AddNextElement(node, tail);
      tail = node;
      Print();
    }

    public void AddAtPosition(int data, int pos)
    {
      bool isPosCorrect = (pos <= size + 1 && pos >= 1);
      if (!isPosCorrect)
      {
        PrintError($"Invalid position. Try [1; {size + 1}]");
        return;
      }
      if (pos == size + 1)
      {
        AddLast(data);
        return;
      }
      else if (pos == 1)
      {
        AddFirst(data);
        return;
      }

      DLNode node = new DLNode(data);
      DLNode current = head;

      if (current != null)
      {
        for (int i = 1; i < pos; ++i)
        {
          current = current.next;
        }
      }
      AddPrevElement(node, current);
      this.Print();
    }

    public void DeleteFirst()
    {
      if (IsHeadNull())
      {
        return;
      }

      RemoveToSize();
      if (tail == head)
      {
        tail = null;
        head = null;
        return;
      }
      tail.next = head.next;
      head.next.prev = tail;
      head = head.next;
      this.Print();
    }

    public void DeleteLast()
    {
      if (IsHeadNull())
      {
        return;
      }
      RemoveToSize();
      if (tail == head)
      {
        tail = null;
        head = null;
        return;
      }

      tail.prev.next = head;
      head.prev = tail.prev;
      tail = tail.prev;
      this.Print();
    }

    public void DeleteAtPos(int pos)
    {
      if (IsHeadNull())
      {
        return;
      }
      DLNode current = head;
      if (!IsPosCorrect(pos))
      {
        PrintError("Invalid index");
        return;
      }
      if (pos == 1)
      {
        DeleteFirst();
        return;
      }
      if (pos == size)
      {
        DeleteLast();
        return;
      }
      for (int i = 1; i < pos; ++i)
      {
        current = current.next;
      }
      current.prev.next = current.next;
      current.next.prev = current.prev;

      RemoveToSize();
      Print();
    }

    public SLList DeleteNodesWithData(int data)
    {
      SLList list = new SLList();
      int s = size;
      var current = head;
      for (int i = 1; i <= s; ++i)
      {
        if (current.data < data)
        {
          list.AddFirst(current.data);
          this.DeleteAtPos(i);
          i -= 1;
        }
        current = current.next;
      }
      return list;
    }

    public int GetDataAtPos(int pos)
    {
      if (!IsPosCorrect(pos))
      {
        PrintError("Invalid pos.");
        return 0;
      }
      var cur = head;
      while (pos > 1)
      {
        cur = cur.next;
        pos -= 1;
      }
      return cur.data;
    }

    public void AddAfterMid(int data)
    {
      int s = size;
      int mid = (s % 2 == 0) ? s / 2 + 1: s / 2 + 2;
      if (s == 1)
      {
          mid = 1;
      }
      AddAtPosition(data, mid);
    }

    private bool IsPosCorrect(int pos)
    {
      return pos >= 1 && pos <= size;
    }
  }

}