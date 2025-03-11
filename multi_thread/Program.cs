Console.WriteLine("Hello, World!");


void PrintNumbers(string threadName, int sleepTime) {
    for (int i = 0; i < 5; i++) {
        Console.WriteLine($"Thread {threadName} - Number: {i}");
        Thread.Sleep(sleepTime);
    }
}


//LessonOne();
//LessonTwo(); 
LessonThree();





void LessonThree(){
    //deadlock
    int counter = 0;
    object lock1 = new object();
    object lock2 = new object();

    Thread t1 = new Thread(() => {
        lock(lock1){
            counter = counter + 2;
            Thread.Sleep(1000);
            lock(lock2) {
                counter = counter + 4;
            }
        }
    });

    Thread t2 = new Thread(() => {
        lock(lock2){
            counter = counter + 1;
            Thread.Sleep(1000);
            lock(lock1) {
                counter = counter + 3;
            }
        }
    });

    t1.Start();
    t2.Start();
    t1.Join();
    t2.Join();


    Console.WriteLine("Counter: " + counter);
    Console.WriteLine("Done");
}


void LessonTwo() {

    int counter = 0;
    object locking = new object();

    void IncreaseCounter() {
        for (int i = 0; i < 30000; i++) {
            lock (locking) {
                //what happen if  you remove this lock?
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