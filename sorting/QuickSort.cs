using System;

namespace sorting
{
    public class QuickSort{

        public void Do(){            
            //var data = new int[]{7,6,8,1,2,9,4,3,5};
            var data = new int[]{1,3,2,5,4};
            Sort(data,0,data.Length-1);
            Console.WriteLine("final " + String.Join(",",data));            
        }

        private void Sort(int[] arr,int start, int end){
            if(start < end){
                var idxPartition = SortPartition(arr,0,end);                                        
                idxPartition = idxPartition-1;  
                Sort(arr,start,idxPartition);
                Sort(arr,idxPartition+1,end);                    
            }
        }
        

        private int SortPartition(int[] arr,int start, int end){
            var midValue = arr[end];
            var minIdx = start - 1;
            for(var i = start; i < end ; i++){
                if(arr[i] < midValue){                      
                    minIdx++;
                    swap(arr,minIdx,i);
                }
            }
            minIdx++;            
            swap(arr,minIdx,end);
            return minIdx;
        }

        private void swap(int[] arr, int idx1, int idx2){
            var temp = arr[idx1];
            arr[idx1] = arr[idx2];
            arr[idx2] = temp;
        }
    }
}