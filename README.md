## Documentation.
<br/>

###### This class will help you to handle your database easily and quickly.
1. To implement this class, create new class in your project then copy all the code off class **Hamham.cs** and past it into your class.
2. On top put this namespace `using Hamham`
3. Then you need to fill this variable with your string connection `On initialize`.
```csharp
DB.MConnectionString = "Your connection";
 ```
<br/>

###### Now you can use this class everywhere in your project like these example.

## Table `users`
<br/>

ID | fName | lName | email | country
-- | ----- | ------| ------- | ------
1 | Mohammed | Hamham | dv.hamham@gmail.com | Morocco
2 | Thomas | Aubry | taubry@gmail.com | France
3 | Benard | Perret | bperret@gmail.com | Spain
4 | Jamal | Bensaad | dv.bensaad@gmail.com | Morocco
5 | Rachid | Ettouti | rettouti@gmail.com | Egypt

<br/><br/>
## Insert new user
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    DB.MParam.Add("ID", 6);
    DB.MParam.Add("fName", "Kamal");
    DB.MParam.Add("lName", "Nassiri");
    DB.MParam.Add("email", "k.nassiri@gmail.com");
    DB.MParam.Add("country", "Libya");
    
    DB.Insert("users");
  }
 ```
 ###### `Result`
ID | fName | lName | email | country
-- | ----- | ------| ------- | ------
1 | Mohammed | Hamham | dv.hamham@gmail.com | Morocco
2 | Thomas | Aubry | taubry@gmail.com | France
3 | Benard | Perret | bperret@gmail.com | Spain
4 | Jamal | Bensaad | dv.bensaad@gmail.com | Morocco
5 | Rachid | Ettouti | rettouti@gmail.com | Egypt
**_6_** | **_Kamal_** | **_Nassiri_** | **_k.nassiri@gmail.com_** | **_Libya_**

<br/><br/>
## Delete user
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    DB.Delete("users","ID=1");
    DB.Delete("users", "fName='Benard'");
    DB.Delete("users", "country='Egypt'");
  }
 ```
 ###### `Result`
ID | fName | lName | email | country
-- | ----- | ------| ------- | ------
2 | Thomas | Aubry | taubry@gmail.com | France
4 | Jamal | Bensaad | dv.bensaad@gmail.com | Morocco

<br/><br/>
## Update users
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    // First update
    DB.MParam.Add("ID", 10);
    DB.MParam.Add("fName", "Karim");
    DB.MParam.Add("lName", "Khoumani");
    DB.Update("users", "ID=2");
    
    // Second update
    DB.MParam.Add("ID", 60);
    DB.MParam.Add("fName", "Yassine");
    DB.MParam.Add("lName", "Rachidi");
    DB.Update("users", "country='Spain'");
  }
```
 ###### `Result`
ID | fName | lName | email | country
-- | ----- | ------| ------- | ------
1 | Mohammed | Hamham | dv.hamham@gmail.com | Morocco
**_10_** | **_Karim_** | **_Khoumani_** | taubry@gmail.com | France
**_60_** | **_Yassine_** | **_Rachidi_** | bperret@gmail.com | Spain
4 | Jamal | Bensaad | dv.bensaad@gmail.com | Morocco
5 | Rachid | Ettouti | rettouti@gmail.com | Egypt

<br/><br/>
## Select all
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    DB.Select("users");
  }
 ```
 ###### `Result`
ID | fName | lName | email | country
-- | ----- | ------| ------- | ------
1 | Mohammed | Hamham | dv.hamham@gmail.com | Morocco
2 | Thomas | Aubry | taubry@gmail.com | France
3 | Benard | Perret | bperret@gmail.com | Spain
4 | Jamal | Bensaad | dv.bensaad@gmail.com | Morocco
5 | Rachid | Ettouti | rettouti@gmail.com | Egypt

<br/><br/>
## Select specific columns
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    DB.Select("users","fName, lName");
  }
 ```
 ###### `Result`
fName | lName 
-- | -----
Mohammed | Hamham
Thomas | Aubry
Benard | Perret
Jamal | Bensaad
Rachid | Ettouti

<br/><br/>
## Search in all columns
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    var textBox = "Jama";
    DB.Select("users","*",$"fName LIKE %{ textBox }% OR lName LIKE %{ textBox }% OR email LIKE %{ textBox }% OR country LIKE %{ textBox }%");
  }
 ```
 ###### `Result`
ID | fName | lName | email | country
-- | ----- | ------| ------- | ------
4 | Jamal | Bensaad | dv.bensaad@gmail.com | Morocco

<br/><br/>
## Multi search
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    var fn = "Rachi";
    var ln = "Ettouti";
    var em = "rettouti";
    var co = "gyp";
    DB.Select("users","fName, email",$"fName LIKE %{ fn }% AND lName LIKE %{ ln }% AND email LIKE %{ em }% AND country LIKE %{ co }%");
  }
 ```
  ###### `Result`
fName | email 
-- | ----- 
Rachid | rettouti@gmail.com

<br/><br/>
## Get number of rows
<br/>

```csharp
  private void button_Click(object sender, EventArgs e)
  {
    var count = DB.Select("users").Rows.Count;
    MessageBoxshow(count.ToString());  
  }
 ```
  ###### `Result`
Alert with number of rows `5`

<br/><br/>
## Example login
<br/>

```csharp
var textBoxEmail    = "dv.hamham@gmail.com";
var textBoxPassword = 123;

var user = DB.Select("users", "*", $"columnEmail={ textBoxEmail } && columnPassword={ textBoxPassword }");
if(user.Rows.Count > 0)
{
    MessageBox.Show("Welcome");
}
else
{
    MessageBox.Show("Email or password incorrect");
}
```

## Author

* [Facebook Profile](https://web.facebook.com/dvhamham)
* [GitHub Profile](https://github.com/dvhamham)


## License

Copyright © 2021, [Mohamed Hamham](http://hamham.me/). Released under the [MIT License](https://github.com/dvhamham/CsharpDB/blob/main/LICENSE).
