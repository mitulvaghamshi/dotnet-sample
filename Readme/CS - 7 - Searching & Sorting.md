## CS - Searching & Sorting

- Searching data involves determining whether a value (referred to as the search key) is present in the data and, if so, finding the value's location.
- Two popular search algorithms are the simple linear search and the faster, but more complex, binary search.
- Sorting places data in order, based on one or more sort keys.

### Index

| No  | Topic                                                           |
| :-: | :-------------------------------------------------------------- |
|  1  | [Searching and Sorting](#searching-and-sorting)                 |
|  2  | [Linear Search](#linear-search)                                 |
|  3  | [Efficiency of Linear Search](#efficiency-of-linear-search)     |
|  4  | [Constant Runtime](#constant-runtime)                           |
|  5  | [Linear Runtime](#linear-runtime)                               |
|  6  | [Quadratic Runtime](#quadratic-runtime)                         |
|  7  | [Linear Search Runtime](#linear-search-runtime)                 |
|  8  | [Binary Search](#binary-search)                                 |
|  9  | [Efficiency of Binary Search](#efficiency-of-binary-search)     |
| 10  | [Sorting Algorithms](#sorting-algorithms)                       |
| 11  | [Selection Sort](#selection-sort)                               |
| 12  | [Insertion Sort](#insertion-sort)                               |
| 13  | [Merge Sort](#merge-sort)                                       |
| 14  | [Efficiency of Algorithms](#efficiency-of-algorithms)           |
| 15  | [Searching algorithms Big O](#searching-algorithms-big-o)       |
| 16  | [Sorting Algorithms Complexity](#sorting-algorithms-complexity) |

## Linear Search

- The linear search algorithm searches each element in an array sequentially.
- If the search key does not match an element in the array, the algorithm tests each element and, when the end of the array is reached, informs the user that the search key is not present.
- If the search key is in the array, the algorithm tests each element until it finds one that matches the search key and returns the index of that element.
- As an example, consider an array containing $ 34, 56, 2, 10, 77, 51, 93, 30, 5, 52\` and a method that is searching for 51.
- Using the linear search algorithm, the method first checks whether 34 matches the search key.
- It does not, so the algorithm checks whether 56 matches the search key.
- The method continues moving through the array sequentially, testing 2, then 10, then 77.
- When the method tests 51, which matches the search key, the method returns the index 5, which is the location of 51 in the array.
- If, after checking every array element, the method determines that the search key does not match any element in the array, the method returns -1.
- If there are duplicate values in the array, linear search returns the index of the first element in the array that matches the search key.

### Efficiency of Linear Search

- Searching algorithms all accomplish the same goal, finding an element that matches a given search key, if such an element exists.
- Many things, however, differentiate search algorithms from one another.
- The major difference is the amount of effort required to complete the search.
- One way to describe this effort is with Big O notation, which is a measure of the worst-case runtime for an algorithm, that is, how hard an algorithm may have to work to solve a problem.
- For searching and sorting algorithms, this is particularly dependent on how many elements there are in the data set and the algorithm used.

### Constant Runtime

- Suppose an algorithm is designed to test whether the first element of an array is equal to the second element.
- If the array has 10 elements, this algorithm requires one comparison.
- If the array has 1,000 elements, this algorithm still requires one comparison.
- In fact, this algorithm is completely independent of the number of elements in the array, and is thus said to have a constant runtime, which is represented in Big O notation as O(1).
- An algorithm that is O(1) does not necessarily require only one comparison.
- O(1) just means that the number of comparisons is constant, it does not grow as the size of the array increases.
- An algorithm that tests whether the first element of an array is equal to any of the next three elements is still O(1), even though it requires three comparisons.

### Linear Runtime

- An algorithm that tests whether the first element of an array is equal to any of the other elements of the array will require at most n–1 comparisons, where n is the number of elements in the array.
- If the array has 10 elements, this algorithm requires up to nine comparisons.
- If the array has 1,000 elements, this algorithm requires up to 999 comparisons.
- As n grows larger, the n part of the expression dominates, and subtracting one becomes inconsequential.
- Big O is designed to highlight these dominant terms and ignore terms that become unimportant as n grows.
- For this reason, an algorithm that requires a total of n–1 comparisons (such as the one we described earlier) is said to be _O_(_n_).
- An O(n) algorithm is referred to as having a linear runtime.
- O(n) is often pronounced "on the order of n" or more simply "order n."

### Quadratic Runtime

- Now suppose you have an algorithm that tests whether any element of an array is duplicated elsewhere in the array.
- The first element must be compared with every other element in the array.
- The second element must be compared with every other element except the first (it was already compared to the first).
- The third element must be compared with every other element except the first two.
- In the end, this algorithm will end up making (_n_–1)+(_n_–2)+⋯+2+1 or (*n*2÷2)−(_n_÷2) comparisons.
- As n increases, the 2_n2_ term dominates and the n term becomes inconsequential.
- Again, _BigO_ notation highlights the *n*2 term, leaving *n*2/2.
- But as we'll soon see, constant factors are omitted in _BigO_ notation.
- _BigO_ is concerned with how an algorithm's runtime grows in relation to the number of items processed.
- Suppose an algorithm requires *n*2 comparisons.
- With four elements, the algorithm will require 16 comparisons; with eight elements, the algorithm will require 64 comparisons.
- With this algorithm, doubling the number of elements quadruples the number of comparisons.
- Consider a similar algorithm requiring *n*2/2 comparisons.
- With four elements, the algorithm will require eight comparisons; with eight elements, 32 comparisons.
- Again, doubling the number of elements quadruples the number of comparisons.
- Both of these algorithms grow as the square of n, so _BigO_ ignores the constant, and both algorithms are considered to be _O_(*n*2), referred to as quadratic runtime and pronounced "on the order of n-squared" or more simply "order n-squared."
- When n is small, _O_(*n*2) algorithms (running on today's billions-of-operations-per second personal computers) will not noticeably affect performance.
- But as n grows, you'll start to notice the performance degradation.
- An _O_(*n*2) algorithm running on a million-element array would require a trillion "operations" (where each could actually require several machine instructions to execute).
- This could require many minutes to execute.
- A billion-element array would require a quintillion operations, a number so large that the algorithm could take decades!
- _O_(*n*2) algorithms are easy to write, as you'll see shortly.
- You'll also see algorithms with more favorable _BigO_ measures.
- These efficient algorithms often take more cleverness and effort to create, but their superior performance can be well worth the extra effort, especially as n gets large.

## Linear Search Runtime

- The linear search algorithm runs in O(n) time.
- The worst case in this algorithm is that every element must be checked to determine whether the search item exists in the array.
- If the size of the array is doubled, the number of comparisons that the algorithm must perform is also doubled.
- Linear search can provide outstanding performance if the element matching the search key happens to be at or near the front of the array.
- But we seek algorithms that perform well, on average, across all searches, including those where the element matching the search key is near the end of the array.
- Linear search is the easiest search algorithm to program, but it can be slow compared to other search algorithms.
- If an app needs to perform many searches on large arrays, it may be better to implement a different, more efficient algorithm, such as the binary search, which we present in the next section.
- Sometimes the simplest algorithms perform poorly.
- Their virtue is that they're easy to program, test and debug.
- Sometimes more complex algorithms are required to realize maximum performance.

## Binary Search

- The binary search algorithm is more efficient than the linear search algorithm, but it requires that the array first be sorted.
- The first iteration of this algorithm tests the middle element in the array.
- If this matches the search key, the algorithm ends.
- Assuming the array is sorted in ascending order, if the search key is less than the middle element, the search key cannot match any element in the second half of the array and the algorithm continues with only the first half of the array (i.e., the first element up to, but not including, the middle element).
- If the search key is greater than the middle element, the search key cannot match any element in the first half of the array, and the algorithm continues with only the second half of the array (i.e., the element after the middle element through the last element).
- Each iteration tests the middle value of the remaining portion of the array, called a subarray.
- A subarray can have no elements, or it can encompass the entire array.
- If the search key does not match the element, the algorithm eliminates half of the remaining elements.
- The algorithm ends by either finding an element that matches the search key or reducing the subarray to zero size.
- As an example, consider the sorted 15-element array 2 3 5 10 27 30 34 51 56 65 77 81 82 93 99 and a search key of 65.
- An app implementing the binary search algorithm would first check whether 51 is the search key (because 51 is the middle element of the array).
- The search key (65) is larger than 51, so 51 is "discarded" (i.e., eliminated from consideration) along with the first half of the array (all elements smaller than 51).
- Next, the algorithm checks whether 81 (the middle element of the remainder of the array) matches the search key.
- The search key (65) is smaller than 81, so 81 is discarded along with the elements larger than 81.
- After just two tests, the algorithm has narrowed the number of values to check to three (56, 65 and 77).
- The algorithm then checks 65 (which indeed matches the search key) and returns the index of the array element containing 65.
- This algorithm required just three comparisons to determine whether the search key matched an element of the array.
- Using a linear search algorithm would have required 10 comparisons.
- In this example, we have chosen to use an array with 15 elements so that there will always be an obvious middle element in the array.
- With an even number of elements, the middle of the array lies between two elements.
- We implement the algorithm to choose the higher of the two elements.
- Recall that the binary search algorithm works only on sorted arrays.
- Loop until the user enters -1.
- For each other number the user enters, the app performs a binary search to determine whether the number matches an element in the array.
- The first line of output from this app is the array of ints, in increasing order.
- When the user instructs the app to search for 72, the app first tests the middle element (indicated by * in the sample output), which is 52.
- The search key is greater than 52, so the app eliminates from consideration the first half of the array and tests the middle element from the second half.
- The search key is smaller than 82, so the app eliminates from consideration the second half of the subarray, leaving only three elements.
- Finally, the app checks 72 (which matches the search key) and returns the index 9.

### Efficiency of Binary Search

- In the worst-case scenario, searching a sorted array of 1,023 elements will take only 10 comparisons when using a binary search.
- Repeatedly dividing 1,023 by 2 (because after each comparison, we are able to eliminate half of the array) and rounding down (because we also remove the middle element) yields the values
  - `511, 255, 127, 63, 31, 15, 7, 3, 1, 0`.
- The number 1023 (210 – 1) is divided by 2 only 10 times to get the value 0, which indicates that there are no more elements to test.
- Dividing by 2 is equivalent to one comparison in the binary search algorithm.
- Thus, an array of 1,048,575 (220 – 1) elements takes a maximum of 20 comparisons to find the key, and an array of one billion elements (which is less than 230 – 1) takes a maximum of 30 comparisons to find the key.
- This is a tremendous improvement in performance over the linear search.
- The maximum number of comparisons needed for the binary search of any sorted array is the exponent of the first power of 2 greater than the number of elements in the array, which is represented as *log*2(_n_).
- All logarithms grow at roughly the same rate, so in _BigO_ notation the base can be omitted.
- This results in a _BigO_ of _O_(_log_(_n_)) for a binary search, which is also known as logarithmic runtime.

## Sorting Algorithms

- Sorting data (i.e., placing the data in some particular order, such as ascending or descending) is one of the most important computing applications.
- Virtually every organization must sort some data often, massive amounts of it.
- Sorting data is an intriguing, compute-intensive problem that has attracted substantial research efforts.
- It's important to understand about sorting that the end result—the sorted array will be the same no matter which (correct) algorithm you use to sort the array.
- The choice of algorithm affects only the runtime and memory use of the app.
- The rest of the chapter introduces three common sorting algorithms.
- The first two—selection sort and insertion sort are simple to program, but inefficient.
- The last—merge sort—is much faster than selection sort and insertion sort but more difficult to program. We focus on sorting arrays of simple-type data, namely ints.
- It's possible to sort arrays of objects as well.

## Selection Sort

- Selection sort is a simple, but inefficient, sorting algorithm.
- The first iteration of the algorithm selects the smallest element in the array and swaps it with the first element.
- The second iteration selects the second-smallest element (which is the smallest of the remaining elements) and swaps it with the second element.
- The algorithm continues until the last iteration selects the second-largest element and, if necessary, swaps it with the second-to-last element, leaving the largest element in the last position.
- After the ith iteration, the smallest i elements of the array will be sorted in increasing order in the first i positions of the array.
- As an example, consider the array `34 56 4 10 77 51 93 30 5 52`
- An app that implements selection sort first determines the smallest element (4) of this array, which is contained in index 2 (i.e., position 3).
- The app swaps 4 with 34, resulting in `4 56 34 10 77 51 93 30 5 52`
- The app then determines the smallest value of the remaining elements (all elements except 4), which is 5, contained in index 8.
- The app swaps 5 with 56, resulting in `4 5 34 10 77 51 93 30 56 52`
- On the third iteration, the app determines the next smallest value (10) and swaps it with 34.
- `4 5 10 34 77 51 93 30 56`
- The process continues until the array is fully sorted.
- `4 5 10 30 34 51 52 56 77 93`
- After the first iteration, the smallest element is in the first position.
- After the second iteration, the two smallest elements are in order in the first two positions.
- After the third iteration, the three smallest elements are in order in the first three positions.
- Line 22 calls method SelectionSort, which sorts the elements using selection sort.
- The output uses dashes to indicate the portion of the array that is sorted after each pass.
- An asterisk is placed next to the position of the element that was swapped with the smallest element on that pass.
- On each pass, the element next to the asterisk and the element above the rightmost set of dashes were the two values that were swapped.

## Insertion Sort

- Insertion sort is another simple, but inefficient, sorting algorithm.
- Its first iteration takes the second element in the array and, if it's less than the first, swaps them.
- The second iteration looks at the third element and inserts it in the correct position with respect to the first two elements (moving them as necessary), so all three elements are in order.
- At the ith iteration of this algorithm, the first i elements in the original array will be sorted.
- Consider as an example the following array, which is identical to the array we used in the discussions of selection sort and merge sort.
- `34 56 4 10 77 51 93 30 5 52`
- An app that implements the insertion sort algorithm first looks at the first two elements of the array, 34 and 56.
- These are already in order, so the app continues (if they were out of order, it would swap them).
- In the next iteration, the app looks at the third value, 4.
- This value is less than 56, so the app stores 4 in a temporary variable and moves 56 one element to the right.
- The app then checks and determines that 4 is less than 34, so it moves 34 one element to the right.
- The app has now reached the beginning of the array, so it places 4 in the zeroth position.
- The array now `is 4 34 56 10 77 51 93 30 5 52`
- In the next iteration, the app stores the value 10 in a temporary variable.
- Then the app compares 10 to 56 and moves 56 one element to the right because it's larger than 10.
- The app then compares 10 to 34, moving 34 one element to the right. When the app compares 10 to 4, it observes that 10 is larger than 4 and places 10 in element 1.
- The array now is `4 10 34 56 77 51 93 30 5 52`
- Using this algorithm, at the ith iteration, the original array's first i elements are sorted, but they may not be in their final locations, smaller values may be located later in the array.

## Merge Sort

- Merge sort is an efficient sorting algorithm but is conceptually more complex than selection sort and insertion sort.
- The merge sort algorithm sorts an array by splitting it into two equal sized subarrays, sorting each subarray and merging them in one larger array.
- With an odd number of elements, the algorithm creates the two subarrays such that one has one more element than the other.
- The implementation of merge sort in this example is recursive. The base case is an array with one element.
- A one-element array is, of course, sorted, so merge sort immediately returns when it's called with a one-element array.
- The recursion step splits an array in two approximately equal-length pieces, recursively sorts them and merges the two sorted arrays in one larger, sorted array.
- Suppose the algorithm has already merged smaller arrays to create sorted arrays
- `A: [4 10 34 56 77]` and, `B: [5 30 51 52 93]`
- Merge sort combines these two arrays in one larger, sorted array.
- The smallest element in A is 4 (located in the zeroth element of A).
- The smallest element in B is 5 (located in the zeroth element of B).
- In order to determine the smallest element in the larger array, the algorithm compares 4 and 5.
- The value from A is smaller, so 4 becomes the first element in the merged array.
- The algorithm continues by comparing 10 (the second element in A) to 5 (the first element in B).
- The value from B is smaller, so 5 becomes the second element in the larger array.
- The algorithm continues by comparing 10 to 30, with 10 becoming the third element in the array, and so on.
- As you may have noticed, the key to achieving the high efficiency of the merge sort is cleverly using additional memory.
- This is another example of the spacetime trade-off. If you have more space you can get your algorithm to run in less time and if you have less space your algorithm may require more time.
- In industry, you might create apps for memory constrained devices, which might prevent you from using the merge sort algorithm.

# Efficiency of Algorithms

## Searching Algorithms

### Linear Search:

Linear search is a simple searching algorithm that sequentially checks each element in a list or array until it finds the target value or reaches the end of the list. It is also known as a sequential search.

Time Complexity: O(n)

### Binary Search:

Binary search is an efficient searching algorithm that works on sorted lists or arrays. It follows a divide-and-conquer approach, repeatedly dividing the search space in half until the target element is found or determined to be absent.

Time Complexity: O(log2 n)

Sorting Algorithms

### Bubble Sort:

Bubble sort is a simple sorting algorithm that repeatedly steps through the list or array, compares adjacent elements, and swaps them if they are in the wrong order. The algorithm gets its name from the way smaller elements "bubble" to the top of the list with each iteration.

Time Complexity: O(n2)

### Insertion Sort:

Insertion sort is a simple sorting algorithm that builds the final sorted array one element at a time. It iterates through the input list or array and gradually expands a sorted portion of the list by inserting each new element in its proper place.

Time Complexity: O(n2)

### Selection Sort:

Selection sort is a simple sorting algorithm that divides the input list into two parts: the sorted portion at the beginning and the unsorted portion at the end. It repeatedly selects the smallest element from the unsorted portion and swaps it with the element at the beginning of the unsorted portion, thus expanding the sorted portion by one element.

Time Complexity: O(n2)

### Heap Sort:

Heap sort is a comparison-based sorting algorithm that uses a binary heap data structure to sort elements. It involves building a heap from the input list and repeatedly extracting the maximum (for ascending order) or minimum (for descending order) element from the heap and placing it at the end of the sorted portion of the list.

Time Complexity: O(n*log n)

### Merge Sort:

Merge sort is a recursive sorting algorithm that follows the divide-and-conquer principle. It divides the input list or array into smaller subproblems, sorts them independently, and then merges them to obtain a sorted result.

Time Complexity: O(n*log n)

### Quick Sort:

Quick sort is a highly efficient, comparison-based sorting algorithm that follows the divide-and-conquer principle. It works by partitioning the input list or array into two subarrays based on a chosen pivot element, and recursively sorting the subarrays.

Time Complexity: O(n*log n)

## Graph Algorithms

### Breadth-first search:

Breadth-first search is an algorithm for searching a tree data structure for a node that satisfies a given property. It starts at the tree root and explores all nodes at the present depth prior to moving on to the nodes at the next depth level

Time Complexity: O(V + E)

### Depth-first search:

Depth-first search is an algorithm for traversing or searching tree or graph data structures. The algorithm starts at the root node and explores as far as possible along each branch before backtracking.

Time Complexity: O(V + E)

### Dijkstra's algorithm

Dijkstra's algorithm is an algorithm for finding the shortest paths between nodes in a weighted graph, which may represent, for example, road networks. It was conceived by computer scientist Edsger W. Dijkstra in 1956 and published three years later. The algorithm exists in many variants

Time Complexity: O (V + E log V)

### Kadane's algorithm

Kadane's algorithm is an efficient algorithm used to find the maximum sum of a contiguous subarray within a given array of numbers. It is often used to solve the maximum subarray problem.

Time Complexity: O(n)

## Tree algorithm

### Inorder Traversal:

- In an inorder traversal, the left subtree is visited first, followed by the root node, and then the right subtree.
- The inorder traversal visits the nodes of a binary tree in ascending order when applied to a binary search tree (BST).
- The pseudocode for inorder traversal is as follows:

Time Complexity: O(n)

### Preorder Traversal:

- In a preorder traversal, the root node is visited first, followed by the left subtree, and then the right subtree.
- The preorder traversal is useful for creating a copy of a tree, as the nodes are visited in the order they appear in the original tree.
- The pseudocode for preorder traversal is as follows:

Time Complexity: O(n)

### Postorder Traversal:

- In a postorder traversal, the left subtree is visited first, followed by the right subtree, and finally the root node.
- The postorder traversal is commonly used to delete nodes from a tree since it ensures that a node's children are deleted before the node itself.
- The pseudocode for postorder traversal is as follows:

Time Complexity: O(n)

## Searching algorithms Big O

| Searching Algorithms    | _Big O_     |
| :---------------------- | ----------- |
| Linear Search           | _O(n)_      |
| Binary Search           | _O(log(n))_ |
| Recursive Linear Search | _O(n)_      |
| Recursive Binary Search | _O(log(n))_ |

## Number of comparisons for common Big O

| _n_            |  1 |  2 |  3 |  4 |  5 | 10 | 100 | 1,000 | 1,000,000 | 1,000,000,000 |
| :------------- | -: | -: | -: | -: | -: | -: | --: | ----: | --------: | ------------: |
| _O(log(n))_    |  0 |  1 |  1 |  1 |  1 |  1 |   2 |     3 |         6 |             9 |
| _O(*n*log(n))_ |  1 |  2 |  3 |  4 |  5 | 10 | 100 | 1,000 | 1,000,000 | 1,000,000,000 |
| _O(*n*2)_      |  0 |  2 |  3 |  4 |  5 | 10 | 200 |  3000 | 6,000,000 | 9,000,000,000 |

## Sorting Algorithms Complexity

| Algorithm          | Time Complexity |               |               | Space Complexity |
| :----------------- | :-------------- | :------------ | :------------ | :--------------- |
|                    | **Best**        | **Average**   | **Worst**     | **Worst**        |
| **Quicksort**      | O(n⋅log(n))     | O(n⋅log(n))   | O(n2)         | O(log(n))        |
| **Mergesort**      | O(n⋅log(n))     | O(n⋅log(n))   | O(n⋅log(n))   | O(n)             |
| **Timsort**        | O(n)            | O(n⋅log(n))   | O(n⋅log(n))   | O(n)             |
| **Heapsort**       | O(n⋅log(n))     | O(n⋅log(n))   | O(n⋅log(n))   | O(1)             |
| **Bubble Sort**    | O(n)            | O(n2)         | O(n2)         | O(1)             |
| **Insertion Sort** | O(n)            | O(n2)         | O(n2)         | O(1)             |
| **Selection Sort** | O(n2)           | O(n2)         | O(n2)         | O(1)             |
| **Tree Sort**      | O(n⋅log(n))     | O(n⋅log(n))   | O(n2)         | O(n)             |
| **Shell Sort**     | O(n⋅log(n))     | O(n(log(n))2) | O(n(log(n))2) | O(1)             |
| **Bucket Sort**    | O(n+k)          | O(n+k)        | O(n2)         | O(n)             |
| **Radix Sort**     | O(n⋅k)          | O(n⋅k)        | O(n⋅k)        | O(n+k)           |
| **Counting Sort**  | O(n+k)          | O(n+k)        | O(n+k)        | O(k)             |
| **Cubesort**       | O(n)            | O(n⋅log(n))   | O(n⋅log(n))   | O(n)             |

## Big O Complexity Chart

![Big O Complexity Chart][cs-chart]

[cs-chart]: <data:image/webp;base64,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>
