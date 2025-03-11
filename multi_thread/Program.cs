Console.WriteLine("Hello, World!");


void PrintNumbers(string threadName, int sleepTime) {
    for (int i = 0; i < 5; i++) {
        Console.WriteLine($"Thread {threadName} - Number: {i}");
        Thread.Sleep(sleepTime);
    }
}


//LessonOne();
LessonTwo(); 




void LessonTwo() {

    int counter = 0;
    object locking = new object();

    void IncreaseCounter() {
        for (int i = 0; i < 30000; i++) {
            lock (locking) {
                counter++;
            }
        }
    }

    Thread t1 = new Thread( () => IncreaseCounter());
    Thread t2 = new Thread( () => IncreaseCounter());
    Thread t3 = new Thread( () => IncreaseCounter());

    t1.Start();
    t2.Start();
    t3.Start();

    t1.Join();
    t2.Join();
    t3.Join();


    Console.WriteLine("Counter: " + counter);
    Console.WriteLine("Done");
}

void LessonOne() {
    Thread t1 = new Thread(() => PrintNumbers("Thread 1", 1000));
    Thread t2 = new Thread(() => PrintNumbers("Thread 2", 500));
    Thread t3 = new Thread(() => PrintNumbers("Thread 3", 100));

    t1.Start();
    t2.Start();
    t3.Start();

    t1.Join();
    t2.Join();
    t3.Join();


    Console.WriteLine("Done");
    Console.ReadKey();
}