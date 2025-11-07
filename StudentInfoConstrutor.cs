using System;


class Student
{
    string name;
    int id;
    double cgpa;

    
    public Student(string Name, int id, double cgpa) //creat filed
    {
        name = Name;
        this.id = id;
        this.cgpa = cgpa;

    }

    void Display()
    {
        Console.WriteLine("Student Name: " + name);
        Console.WriteLine("Student ID: " + id);
        Console.WriteLine("Student CGPA: " + cgpa);

    }
    
    static void Main()
    {

        Student student1 = new Student("Saiful Khan", 1026, 3.58);
        student1.Display();
        Student student2 = new Student("Shakir Molla", 1033, 3.22);
        student2.Display();
        Student student3 = new Student("Shupria Khan", 1027, 3.09);
        student3.Display();
        Student student4 = new Student("Shava Khan", 1028, 3.07);
        student4.Display();

    }

}
