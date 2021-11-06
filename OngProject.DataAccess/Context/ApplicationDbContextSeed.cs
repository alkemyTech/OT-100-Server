using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            // Create roles
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(userRole);

            context.Roles.Add(new Role
            { Description = userRole.Name, Name = userRole.Name, IdentityId = new Guid(userRole.Id) });
            context.Roles.Add(new Role
            { Description = adminRole.Name, Name = adminRole.Name, IdentityId = new Guid(adminRole.Id) });


            // Create Admin
            var admin = new IdentityUser { UserName = "admin@localhost", Email = "admin@localhost" };

            await userManager.CreateAsync(admin, password: "Abc123.");
            await userManager.AddToRoleAsync(admin, adminRole.Name);
        }

        // Create Activity
        public static async Task SeedDefaultActivityAsync(ApplicationDbContext context)
        {
            if (!context.Activities.Any())
            {
                context.Activities.Add(new Activity
                {
                    Name = "Programas Educativos",
                    Content = "Mediante nuestros programas educativos, buscamos incrementar la capacidad"
                               + "intelectual, moral y afectiva de las personas de acuerdo con la cultura y las"
                                + "normas de convivencia de la sociedad a la que pertenecen.",
                    Image = "image.png"
                });
                context.Activities.Add(new Activity
                {
                    Name = "Apoyo Escolar para el nivel Primario",
                    Content = "El espacio de apoyo escolar es el corazón del área educativa. Se realizan los"
                   + "talleres de lunes a jueves de 10 a 12 horas y de 14 a 16 horas en el contraturno,"
                   + "Los sábados también se realiza el taller para niños y niñas que asisten a la escuela"
                    + "doble turno.Tenemos un espacio especial para los de 1er grado 2 veces por"
                    + "semana ya que ellos necesitan atención especial!"
                    + "Actualmente se encuentran inscriptos a este programa 150 niños y niñas de 6 a 15 años.Este taller está"
                    + "pensado para ayudar a los alumnos con el material que traen de la escuela,"
                   + "también tenemos una docente que les da clases de lengua y matemática con una"
                    + "planificación propia que armamos en Manos para nivelar a los niños y que vayan"
                    + "con más herramientas a la escuela.",
                    Image = "image.png"
                });
                context.Activities.Add(new Activity
                {
                    Name = "Apoyo Escolar Nivel Secundaria",
                    Content = "Del mismo modo que en primaria, este taller es el corazón del área secundaria. Se"
                           + "realizan talleres de lunes a viernes de 10 a 12 horas y de 16 a 18 horas en el"
                           + "contraturno.Actualmente se encuentran inscriptos en el taller 50 adolescentes"
                           + "entre 13 y 20 años.Aquí los jóvenes se presentan con el material que traen del"
                           + "colegio y una docente de la institución y un grupo de voluntarios los recibe para"
                           + "ayudarlos a estudiar o hacer la tarea.Este espacio también es utilizado por los"
                           + "jóvenes como un punto de encuentro y relación entre ellos y la institución.",
                    Image = "image.png"
                });
                context.Activities.Add(new Activity
                {
                    Name = "Tutorias",
                    Content = "Es un programa destinado a jóvenes a partir del tercer año de secundaria, cuyo"
                               + "objetivo es garantizar su permanencia en la escuela y construir un proyecto de"
                               + "vida que da sentido al colegio.El objetivo de esta propuesta es lograr la"
                              + "integración escolar de niños y jóvenes del barrio promoviendo el soporte"
                               + "socioeducativo y emocional apropiado, desarrollando los recursos institucionales"
                               + "necesarios a través de la articulación de nuestras intervenciones con las escuelas que los alojan,"
                               + "con las familias de los alumnos y con las instancias municipales,"
                                + "provinciales y nacionales que correspondan.",
                    Image = "image.png"
                });
                context.Activities.Add(new Activity
                {
                    Name = "Taller Arte y Cuentos",
                    Content = "Taller literario y de manualidades que se realiza semanalmente",
                    Image = "image.png"
                });
                context.Activities.Add(new Activity
                {
                    Name = "Paseos recreativos y educativos",
                    Content = "Estos paseos están pensados para promover la participación y sentido de"
                                + "pertenencia de los niños, niñas y adolescentes al área educativa.",
                    Image = "image.png"
                });
            }
            await context.SaveChangesAsync();

        }



    }
}