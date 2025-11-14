using System;
class Student
{
    string name;
    int id;
    double cgpa;

    //constructor overloading
    public Student()
    {
        name = "Unknown";
        id = 0;
        cgpa = 0.0;
    }

    public Student(string name, int id)
    {
        this.name = name;
        this.id = id;
    }
    public Student(string Name, int id, double cgpa) //constructor
    {
        name = Name;
        this.id = id;
        this.cgpa = cgpa;
    }
    //Copy Constructor
    public Student(Student oldstudent)
    {
        name = oldstudent.name;
        id = oldstudent.id;
        cgpa = oldstudent.cgpa;
    }

    //Function Overloading
    void Display()
    {
        Console.WriteLine("Student Name: " + name);
        Console.WriteLine("Student ID: " + id);
        Console.WriteLine("Student CGPA: " + cgpa);

    }
    void Display(int batch)
    {
        Console.WriteLine("Student Name: " + name);
        Console.WriteLine("Student ID: " + id);
        Console.WriteLine("Student CGPA: " + cgpa);
        Console.WriteLine("Batch: " + batch);
    }
    void Display(int batch,char sec)
    {
        Console.WriteLine("Student Name: " + name);
        Console.WriteLine("Student ID: " + id);
        Console.WriteLine("Student CGPA: " + cgpa);
        Console.WriteLine("Batch: " + batch + sec);
    }
    static void Main()
    {
        Student student1 = new Student();
        //student1.Display();
        //student1.Display(62,'A');
        Student student2 = new Student("DEF", 234);
        //student2.Display();
        Student student3 = new Student("EfG", 456, 3.89);
        //student3.Display();
        //student3.Display(62);
        //student3.Display(62, 'A');
        //Student student4 = student3; //shallow copy
        Student student4 = new Student(student3); // deep copy
        student4.id = 555;
        student4.Display();
        student3.Display();
    }

}
