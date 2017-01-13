// -----------------------------------------------------------------------
// <copyright file="UnitTest_Therard.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    [TestFixture]
    public class UnitTest_Therard
    {
       
     
        [Test]
        public void _0001(){
            var th1 = new Thread(new ThreadStart(() =>{
                for (var i = 1; i < 100; i++){
                    Debug.WriteLine("TH1");
                    Thread.Sleep(200);}}));
            var th2 = new Thread(new ThreadStart(() =>{
                for (var i = 1; i < 100; i++){
                    Debug.WriteLine("TH2");
                    Thread.Sleep(50);}}));
            th1.Start(); th2.Start();
            th1.Join(); th2.Join();
            //TH1
            //TH2
            //TH2
            //TH2
            //TH2
            //TH1
        }
        [Test]
        public void _0002(){
            object lockObj = new object();
            var th1 = new Thread(new ThreadStart(() =>{
                for (var i = 1; i < 100; i++){
                    lock (lockObj){
                        Debug.WriteLine("TH1");
                        Thread.Sleep(200);}}}));
            var th2 = new Thread(new ThreadStart(() =>{
                for (var i = 1; i < 100; i++){
                    lock (lockObj){
                        Debug.WriteLine("TH2");
                        Thread.Sleep(50);}}}));
            th1.Start(); th2.Start();
            th1.Join(); th2.Join();
            //TH1
            //TH2
            //TH1
            //TH2
            //TH1
            //TH2
        }
        [Test]
        public void _0003(){
            object lockObjA = new object();
            object lockObjB = new object();
            var th1 = new Thread(new ThreadStart(() =>{
                for (var i = 1; i < 100; i++){
                    lock (lockObjA){
                        Debug.WriteLine("TH1");
                        Thread.Sleep(200);}}}));
            var th2 = new Thread(new ThreadStart(() =>{
                for (var i = 1; i < 100; i++){
                    lock (lockObjB){
                        Debug.WriteLine("TH2");
                        Thread.Sleep(50);}}}));
            th1.Start(); th2.Start();
            th1.Join(); th2.Join();  
            //TH1
            //TH2
            //TH2
            //TH2
            //TH2
            //TH1
        }

        static object LockObj = new object();

        [Test]
        public void _0004()
        {
            var th1 = new Thread(new ThreadStart(() =>
            {
                for (var i = 1; i < 100; i++)
                {
                    lock (LockObj)
                    {
                        Debug.WriteLine("TH1");
                        Thread.Sleep(200);
                    }
                }
            }));
            var th2 = new Thread(new ThreadStart(() =>
            {
                for (var i = 1; i < 100; i++)
                {
                    lock (LockObj)
                    {
                        Debug.WriteLine("TH2");
                        Thread.Sleep(50);
                    }
                }
            }));
            th1.Start(); th2.Start();
            th1.Join(); th2.Join();

        }

[Test]
public void _0005()
{
    var persons = new MockCollection();
    var th1 = new Thread(new ThreadStart(() =>{
        for (var i = 1; i < 100; i++){
            persons.Add(new PersonItem("Homer", "Simpson"));
            Thread.Sleep(200);}}));
    var th2 = new Thread(new ThreadStart(() =>{
        for (var i = 1; i < 100; i++){
            persons.Add(new PersonItem("Bart", "Simpson"));
            Thread.Sleep(50);}}));
    th1.Start(); th2.Start();
    th1.Join(); th2.Join(); 
    //Homer
    //Bart
    //Bart
    //Bart
    //Bart
    //Homer
}
[Test]
public void _0006()
{
    var persons = new MockCollection();
    var th1 = new Thread(new ThreadStart(() =>{
        for (var i = 1; i < 100; i++){
            persons.Add(new PersonItem("Homer", "Simpson"));
            lock((persons as ICollection).SyncRoot){
            Thread.Sleep(200);}}}));
    var th2 = new Thread(new ThreadStart(() =>{
        for (var i = 1; i < 100; i++){
            lock((persons as ICollection).SyncRoot){
            persons.Add(new PersonItem("Bart", "Simpson"));
            Thread.Sleep(50);}}}));
    th1.Start(); th2.Start();
    th1.Join(); th2.Join(); 
    //Homer
    //Bart
    //Homer
    //Bart
    //Homer
    //Bart
}
class PersonItem 
{
    public string First { get; private set; }
    public string Last { get; private set; }
    public PersonItem(string first, string last){
        First = first;
        Last = last;}
    public override string ToString() { return First + " " + Last; }
}
class MockCollection: BindingList<PersonItem> { }
    }
}
