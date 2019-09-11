using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.Domain.Contracts;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;
using Vaccination.Domain.Services;
using Xunit;

namespace xTest
{
    public class GenericServiceTest
    {
        [Fact]
        public async Task DeleteUndefinedResouseReturnNull()
        {
            // arrange
            Expression<Func<PatientDto, bool>> expression = s => s.Id == 999;

            var mapper = new Mock<IMapper>();
            mapper
                .Setup(x => x.Map<Expression<Func<Patient, bool>>>(expression))
                .Returns(x => x.Id == 999);

            var repo = new Mock<IRepository<Patient>>();

            var service = new GenericService<Patient, PatientDto>(repo.Object, mapper.Object, new Mock<IUnitOfWork>().Object);

            // act
            var res = await service.DeleteAsync(expression);

            // assert
            Assert.Null(res);
        }

        [Fact]
        public async Task DeleteCurrectResouseReturnResourse()
        {
            // arrange
            Expression<Func<PatientDto, bool>> expression = s => s.Id == 1;
            var patient = new Patient() { Id = 1 };
            var res = new PatientDto() { Id = 1 };

            var mapper = new Mock<IMapper>();
            mapper
                .Setup(x => x.Map<Expression<Func<Patient, bool>>>(expression))
                .Returns(x => x.Id == 1);
            mapper
                .Setup(x => x.Map<PatientDto>(patient))
                .Returns(res);

            var repo = new Mock<IRepository<Patient>>();
            repo
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                .Returns(Task.FromResult(patient));

            repo
                .Setup(x => x.DeleteAsync(patient))
                .Returns(Task.FromResult(patient));

            var service = new GenericService<Patient, PatientDto>(repo.Object, mapper.Object, new Mock<IUnitOfWork>().Object);

            // act
            var result = await service.DeleteAsync(expression);

            // assert
            Assert.Equal(result, res);
        }

        [Fact]
        public async Task GetAllReturnEmpty()
        {
            // arrange
            var mapper = new Mock<IMapper>();
            var repo = new Mock<IRepository<Patient>>();
            repo
                .Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(Enumerable.Empty<Patient>()));

            var service = new GenericService<Patient, PatientDto>(repo.Object, mapper.Object, new Mock<IUnitOfWork>().Object);

            // act
            var result = await service.GetAllAsync();

            // assert
            Assert.Empty(result);
            Assert.IsAssignableFrom<IEnumerable<PatientDto>>(result);
        }

        [Fact]
        public async Task GetAllReturnTwoEtc()
        {
            // arrange
            var repo = new Mock<IRepository<Patient>>();
            repo
                .Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new List<Patient>() { new Patient(), new Patient() }.AsEnumerable()));

            var mapper = new Mock<IMapper>();
            mapper
                .Setup(x => x.Map<IEnumerable<PatientDto>>(It.IsAny<IEnumerable<Patient>>()))
                .Returns(new List<PatientDto>() { new PatientDto(), new PatientDto() }.AsEnumerable());

            var service = new GenericService<Patient, PatientDto>(repo.Object, mapper.Object, new Mock<IUnitOfWork>().Object);

            // act
            var result = await service.GetAllAsync();

            // assert
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<PatientDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateUndefinedResouseReturnNull()
        {
            // arrange
            Expression<Func<PatientDto, bool>> expression = s => s.Id == 999;
            var pat = new PatientDto();

            var mapper = new Mock<IMapper>();
            mapper
                .Setup(x => x.Map<Expression<Func<Patient, bool>>>(expression))
                .Returns(x => x.Id == 999);

            var repo = new Mock<IRepository<Patient>>();

            var service = new GenericService<Patient, PatientDto>(repo.Object, mapper.Object, new Mock<IUnitOfWork>().Object);

            // act
            var res = await service.UpdateAsync(pat, expression);

            // assert
            Assert.Null(res);
        }

        [Fact]
        public async Task UpdateCurrectResouseReturnResourceDto()
        {
            // arrange
            Expression<Func<PatientDto, bool>> expression = s => s.Id == 999;
            var pat = new PatientDto();
            var domain = new Patient();

            var mapper = new Mock<IMapper>();
            mapper
                .Setup(x => x.Map<Expression<Func<Patient, bool>>>(expression))
                .Returns(x => x.Id == 999);
            mapper
                .Setup(x => x.Map<PatientDto, Patient>(pat, domain))
                .Returns(domain);
            mapper
                .Setup(x => x.Map<PatientDto>(domain))
                .Returns(pat);

            var repo = new Mock<IRepository<Patient>>();
            repo
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                .Returns(Task.FromResult(domain));
            repo
                .Setup(x => x.UpdateAsync(domain))
                .Returns(Task.FromResult(domain));

            var service = new GenericService<Patient, PatientDto>(repo.Object, mapper.Object, new Mock<IUnitOfWork>().Object);

            // act
            var res = await service.UpdateAsync(pat, expression);

            // assert
            Assert.IsType<PatientDto>(res);
            Assert.Equal(pat, res);
        }
    }
}
