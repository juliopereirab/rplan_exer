using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int singleNumber(int[] A, int n)
    {

      //variable where to accumulate repeated values
      List<int> repeatedNumbers = new List<int>();

      //variable to replicate range of numbers up to n
      List<int> listRange = new List<int>();

      //multiple possible comparissons from index i and index i+1
      for(int i=0; i<n; i++){
        listRange.Add(A[i]);

        for(int j=i+1; j<n; j++){

          if(A[i] == A[j]){
            repeatedNumbers.Add(A[i]);
          }
        }
      }

      //reduce list of repeated numbers
      repeatedNumbers = repeatedNumbers.Distinct().ToList();

      //comparisson and removal of repeated numbers from replicated list
      for(int i=n-1; i>=0; i--){
        foreach(int val in repeatedNumbers){
          if(A[i]==val){
            listRange.RemoveAt(i);
          }
        }
      }

      //returning block 
      if(listRange.Count() == 0){
        Console.Write("nothing to return, number of unique numbers: ");
        return 0;
      } else if(listRange.Count() == 1){
        Console.Write("unique value is: ");
        return listRange[0];
      } else {
        Console.Write("more than one unique value, returning only the first: ");        
        return listRange[0];
      }

    }

    static void Main(string[] args)
    {
        // int[] l = {9, 9, 1, 9, 9, 9, 3, 2, 1};
        int[] l = {1, 1, 9, 7, 7, 7, 6, 6, 6, 23, 23, 8, 8};
        // int[] l = {1, 1, 8, 1, 1, 1, 1, 1, 1, 8, 8};



        int b = 9;
        // int b = 7;
        Console.WriteLine(singleNumber(l, b));
    }


}
