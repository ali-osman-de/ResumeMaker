using AutoMapper;
using ResumeMaker.Domain.Dtos;
using ResumeMaker.Domain.Dtos.Section;
using ResumeMaker.Domain.Dtos.SectionCertificate;
using ResumeMaker.Domain.Dtos.SectionEducation;
using ResumeMaker.Domain.Dtos.SectionJob;
using ResumeMaker.Domain.Dtos.SectionProjects;
using ResumeMaker.Domain.Dtos.SectionVolunteer;
using ResumeMaker.Domain.Entities;
using ResumeMaker.Domain.Entities.Certificates;
using ResumeMaker.Domain.Entities.Educations;
using ResumeMaker.Domain.Entities.Jobs;
using ResumeMaker.Domain.Entities.Projects;
using ResumeMaker.Domain.Entities.Skills;
using ResumeMaker.Domain.Entities.Volunteers;

namespace ResumeMaker.Application.Mappers;

public class ResumeMapper : Profile
{
    public ResumeMapper()
    {
        CreateMap<SaveResumeDto, Resume>()
            .ForMember(dest => dest.SkillCategories, opt => opt.MapFrom(src => src.SkillCategoryDtos))
            .ForMember(dest => dest.JobsHistories, opt => opt.MapFrom(src => src.JobHistoryDtos))
            .ForMember(dest => dest.EducationInfos, opt => opt.MapFrom(src => src.EducationInfoDtos))
            .ForMember(dest => dest.ProjectsInfos, opt => opt.MapFrom(src => src.ProjectsInfoDtos))
            .ForMember(dest => dest.CertificatesInfos, opt => opt.MapFrom(src => src.CertificatesInfoDtos))
            .ForMember(dest => dest.VolunteerInfos, opt => opt.MapFrom(src => src.VolunteerInfoDtos))
            .AfterMap((_, dest) =>
            {
                if (dest.CreatedAt == default)
                {
                    dest.CreatedAt = DateTime.UtcNow;
                }
                dest.UpdatedAt = DateTime.UtcNow;
                dest.IsDeleted = false;

                dest.SkillCategories ??= new List<SkillCategory>();
                foreach (var category in dest.SkillCategories)
                {
                    category.Resume = dest;
                    category.TechnologyHubs ??= new List<TechnologyHub>();
                    foreach (var hub in category.TechnologyHubs)
                    {
                        hub.SkillCategory = category;
                    }
                }

                dest.JobsHistories ??= new List<JobsHistory>();
                foreach (var job in dest.JobsHistories)
                {
                    job.Resume = dest;
                    job.JobDescriptions ??= new List<JobDescription>();
                    foreach (var description in job.JobDescriptions)
                    {
                        description.JobsHistory = job;
                    }
                }

                dest.EducationInfos ??= new List<EducationInfo>();
                foreach (var education in dest.EducationInfos)
                {
                    education.Resume = dest;
                }

                dest.ProjectsInfos ??= new List<ProjectsInfo>();
                foreach (var project in dest.ProjectsInfos)
                {
                    project.Resume = dest;
                    project.ProjectDefinitions ??= new List<ProjectDefinitions>();
                    foreach (var definition in project.ProjectDefinitions)
                    {
                        definition.ProjectsInfo = project;
                    }
                }

                dest.CertificatesInfos ??= new List<CertificatesInfo>();
                foreach (var certificate in dest.CertificatesInfos)
                {
                    certificate.Resume = dest;
                }


                dest.VolunteerInfos ??= new List<VolunteerInfo>();
                foreach (var volunteer in dest.VolunteerInfos)
                {
                    volunteer.Resume = dest;
                    volunteer.VolunteerDescriptions ??= new List<VolunteerDescription>();
                    foreach (var description in volunteer.VolunteerDescriptions)
                    {
                        description.VolunteerInfo = volunteer;
                    }

                }
            });

        CreateMap<Resume, SaveResumeDto>()
            .ForMember(dest => dest.SkillCategoryDtos, opt => opt.MapFrom(src => src.SkillCategories ?? new List<SkillCategory>()))
            .ForMember(dest => dest.JobHistoryDtos, opt => opt.MapFrom(src => src.JobsHistories ?? new List<JobsHistory>()))
            .ForMember(dest => dest.EducationInfoDtos, opt => opt.MapFrom(src => src.EducationInfos ?? new List<EducationInfo>()))
            .ForMember(dest => dest.ProjectsInfoDtos, opt => opt.MapFrom(src => src.ProjectsInfos ?? new List<ProjectsInfo>()))
            .ForMember(dest => dest.CertificatesInfoDtos, opt => opt.MapFrom(src => src.CertificatesInfos ?? new List<CertificatesInfo>()))
            .ForMember(dest => dest.VolunteerInfoDtos, opt => opt.MapFrom(src => src.VolunteerInfos ?? new List<VolunteerInfo>()));

        CreateMap<SkillCategoryDto, SkillCategory>()
            .ForMember(dest => dest.TechnologyHubs, opt => opt.MapFrom(src => src.Skills));
        CreateMap<SkillCategory, SkillCategoryDto>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.TechnologyHubs));
        CreateMap<SkillDto, TechnologyHub>().ReverseMap();

        CreateMap<JobHistoryDto, JobsHistory>()
            .ForMember(dest => dest.JobDescriptions, opt => opt.MapFrom(src => src.JobDescriptionDtos));
        CreateMap<JobsHistory, JobHistoryDto>()
            .ForMember(dest => dest.JobDescriptionDtos, opt => opt.MapFrom(src => src.JobDescriptions));
        CreateMap<JobDescriptionDto, JobDescription>().ReverseMap();

        CreateMap<EducationInfoDto, EducationInfo>().ReverseMap();

        CreateMap<ProjectsInfoDto, ProjectsInfo>()
            .ForMember(dest => dest.ProjectDefinitions, opt => opt.MapFrom(src => src.ProjectDefinitionsDtos));
        CreateMap<ProjectsInfo, ProjectsInfoDto>()
            .ForMember(dest => dest.ProjectDefinitionsDtos, opt => opt.MapFrom(src => src.ProjectDefinitions));
        CreateMap<ProjectDefinitionsDto, ProjectDefinitions>().ReverseMap();

        CreateMap<CertificatesInfoDto, CertificatesInfo>().ReverseMap();

        CreateMap<VolunteerInfoDto, VolunteerInfo>()
            .ForMember(dest => dest.VolunteerDescriptions, opt => opt.MapFrom(src => src.VolunteerDescriptionDtos));
        CreateMap<VolunteerInfo, VolunteerInfoDto>()
            .ForMember(dest => dest.VolunteerDescriptionDtos, opt => opt.MapFrom(src => src.VolunteerDescriptions));
        CreateMap<VolunteerDescriptionDto, VolunteerDescription>().ReverseMap();

        CreateMap<Resume, ResumeSummaryDto>().ReverseMap();
    }

}
