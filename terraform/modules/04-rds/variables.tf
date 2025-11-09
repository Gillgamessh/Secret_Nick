################################################################################
# RDS
################################################################################
variable "db_id" {
  description = "The RDS instance identifier, also used as a 'Name' tag for the DB"
  type        = string
}

variable "db_subnet_group_name" {
  description = "The name of the subnet group where RDS instance is placed"
  type        = string
}

variable "db_storage_size" {
  description = "Allocated storage size in GB for the RDS instance"
  type        = number

  validation {
    condition     = var.db_storage_size <= 20
    error_message = "Allocated storage size must be <=20 GB due to AWS Free Tier limitations."
  }
}

variable "db_username" {
  description = "Master username for the database"
  type        = string
  sensitive   = true
}

variable "db_vpc_sg_ids" {
  description = "List of VPC security group IDs for the DB instance"
  type        = list(string)
}

variable "rds_port" {
  description = "Port for the RDS instance"
  type        = number
}

variable "db_engine" {
  description = "The database engine to use for the RDS instance"
  type        = string
}

variable "db_engine_version" {
  description = "The version of the database engine"
  type        = string
}

variable "db_instance_class" {
  description = "The instance type for the RDS instance"
  type        = string
}

variable "vpc_subnet_ids" {
  description = "Subnet IDs for the DB's subnet group"
  type        = list(string)
}
variable "tags" {
  description = "Tags to apply to the resources"
  type        = map(string)
  default     = {}
}
